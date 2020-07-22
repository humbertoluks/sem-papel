using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

using Domain.Dtos;
using Domain.Models;
using Domain.Helpers;
using Repository.Interfaces;
using Service.Interfaces;
using Domain.Dtos.Push;
using Domain.ValueObjects;

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
        private readonly IAssociadoService _AssociadoService ;
        private readonly IPushService _PushService;
        private readonly IUnitOfWork _uow;

        public GuiaController(ILogger<GuiaController> logger, IDiagnosticContext diagnosticContext, 
            [FromServices] IGuiaRepository GuiaRepository, 
            [FromServices] IGuiaNumeroRepository GuiaNumeroRepository,
            [FromServices] IGuiaService GuiaService,
            [FromServices] IPrestadorService PrestadorService,
            [FromServices] IAssociadoService AssociadoService,
            [FromServices] IPushService PushService,
            [FromServices] IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _GuiaRepository = GuiaRepository;
            _GuiaNumeroRepository =GuiaNumeroRepository;
            _GuiaService = GuiaService;
            _PrestadorService = PrestadorService;
            _AssociadoService = AssociadoService;
            _PushService = PushService;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("v1/guia")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]GuiaDto guiaDto)
        {
            try
            {
                var guia = _mapper.Map<GuiaDto, Guia>(guiaDto);
                var prestador = _PrestadorService.PrestadorDescription(guia.Prestador.Codigo);
                var guidePerformerCodeType = Enums.PerformerCodeType.codigoPrestadorNaOperadora;
                // Complemento da Guia
                var guiaNumero = await _GuiaNumeroRepository.GetLastGuiaIdAsync(guia.Prestador.Codigo);
                guia.GuiaNumero.Numero = guiaNumero.ToString();
                guia.GuiaNumero.NumeroOperadora = "";
 
                guia.Beneficiario.Nome = _AssociadoService.SeachAssociado(guia.Beneficiario.Cartao);

                PushRequest request = new PushRequest {
                    Associado = new IntAssociado {
                        CodAcompanhante = "",
                        CodAssociado = guia.Beneficiario.Cartao
                    },
                    Prestador = new IntPrestador {
                        CodPrestador = guia.Prestador.Codigo,
                        NomePrestador = prestador,
                        Endereco = "123",
                        Localizacao = new Localizacao()
                    },
                    CodAtendimento = "193"
                };

                var codigo = _PushService.Post(request);
                
                guia.PushId = codigo;
                guia.TokenId = "";

                guia.GuiaOrigemFK = (int)Enums.SourceInterface.TELEMEDICINA;
                guia.StatusCheckInFK = (int)Enums.StatusCheckIns.Valido;
                guia.GuiaTipoFK = (int)Enums.TypeGuia.Consulta;
                guia.GuiaStatusFK = (int)Enums.StatusGuia.Aberta;

                string profissional = _PrestadorService.PrestadorMedico(guia.Prestador.Codigo, 
                    guiaDto.ProfissionalUFCRM,Convert.ToInt32(guiaDto.ProfissionalCRM), null);
                
                guia.GuiaXML = _GuiaService.GenerateXMLGuia(guia, prestador,
                    guidePerformerCodeType, guiaDto.ProfissionalUFCRM, 
                    Convert.ToInt32(guiaDto.ProfissionalCRM), profissional, guiaDto.Procedimento);
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