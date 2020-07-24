using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

using Domain.Arguments;
using Domain.Dtos;
using Domain.Models;
using Domain.Helpers;
using Repository.Interfaces;
using Service.Interfaces;

namespace Backend.Controllers
{
    [ApiController]
    public class GuiaController : ControllerBase
    {
        public IDiagnosticContext _diagnosticContext { get; }
        private ILogger<GuiaController> _logger;
        private readonly IMapper _mapper;
        private readonly IGuiaRepository _GuiaRepository;
        private readonly IGuiaNumeroRepository _GuiaNumeroRepository;
        private readonly IGuiaService _GuiaService;
        private readonly IPrestadorService _PrestadorService;
        private readonly IBeneficiarioService _BeneficiarioService ;
        private readonly IPushService _PushService;
        private readonly ITokenService _TokenService;
        private readonly IUnitOfWork _uow;
        private readonly IPushRequest _PushRequest;

        public GuiaController(ILogger<GuiaController> logger, IDiagnosticContext diagnosticContext, 
            IGuiaRepository GuiaRepository, 
            IGuiaNumeroRepository GuiaNumeroRepository,
            IGuiaService GuiaService,
            IPrestadorService PrestadorService,
            IBeneficiarioService AssociadoService,
            IPushService PushService,
            ITokenService TokenService,
            IUnitOfWork uow,
            IPushRequest pushRequest,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _GuiaRepository = GuiaRepository;
            _GuiaNumeroRepository =GuiaNumeroRepository;
            _GuiaService = GuiaService;
            _PrestadorService = PrestadorService;
            _BeneficiarioService = AssociadoService;
            _PushService = PushService;
            _TokenService = TokenService;
            _uow = uow;
            _mapper = mapper;
            _PushRequest = pushRequest;
        }

        [HttpPost]
        [Route("v1/guia")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]GuiaDto guiaDto)
        {
            try
            {
                var guia = _mapper.Map<GuiaDto, Guia>(guiaDto);
                var guidePerformerCodeType = Enums.PerformerCodeType.codigoPrestadorNaOperadora;
                // Complemento da Guia
                var guiaNumero = await _GuiaNumeroRepository.GetLastGuiaIdAsync(guia.Prestador.Codigo);
                guia.GuiaNumero.Numero = guiaNumero.ToString();
                guia.GuiaNumero.NumeroOperadora = "";
 
                BeneficiarioResponse beneficiario = _BeneficiarioService.SeachBeneficiario(guia.Beneficiario.Cartao);
                guia.Beneficiario.Nome = beneficiario.guideBeneficiaryName;

                _PushRequest.CodAtendimento = "123"; 
                _PushRequest.Associado.CodAcompanhante = "";
                _PushRequest.Associado.CodAssociado = guia.Beneficiario.Cartao;
                
                var prestador = _PrestadorService.PrestadorDescription(guia.Prestador.Codigo);
                
                _PushRequest.Prestador.CodPrestador = guia.Prestador.Codigo;
                _PushRequest.Prestador.Endereco = "193";
                _PushRequest.Prestador.Localizacao.Latitude  = "";
                _PushRequest.Prestador.Localizacao.Longitude = "";
                _PushRequest.Prestador.NomePrestador   = prestador;

                guia.PushId  = _PushService.GetPushCode(_PushRequest);
                guia.TokenId = _TokenService.GetTokenCode(_PushRequest.Associado.CodAssociado);

                guia.GuiaOrigemFK    = (int)Enums.SourceInterface.Telemedicina;
                guia.StatusCheckInFK = (int)Enums.StatusCheckIns.Valido;
                guia.GuiaTipoFK      = (int)Enums.TypeGuia.Consulta;
                guia.GuiaStatusFK    = (int)Enums.StatusGuia.Aberta;

                MedicoResponse profissional = _PrestadorService.PrestadorMedico(guia.Prestador.Codigo, 
                    guiaDto.ProfissionalUFCRM,Convert.ToInt32(guiaDto.ProfissionalCRM), null);

                guia.GuiaXML = _GuiaService.GenerateXMLGuia(guia, guidePerformerCodeType, 
                    prestador, beneficiario, profissional, guiaDto.Procedimento);
                
                string textGuia = guia.GuiaXML.ToString();

                _logger.LogInformation(textGuia);

                _GuiaRepository.Save(guia);
                await _uow.CommitAsync();
 

                return Created($"/v1/guia/{guia.Id}", null);
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("v1/guia/{GuiaExternaId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(decimal id){
  
            try
            {
                _GuiaRepository.Delete(id);
                await _uow.CommitAsync();

                return Ok();
            } 
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError
                    , $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("v1/guia")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var guias = await _GuiaRepository.All();
                if (guias == null)
                    return NotFound();

                return Ok(guias);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                    , $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("v1/guia/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var guia = await _GuiaRepository.GetByIdAsync(id);

                return base.Ok(guia);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                    , $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }
    }
}
