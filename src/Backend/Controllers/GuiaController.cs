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
using Repository.Interfaces;

namespace Backend.Controllers
{
    [ApiController]
    public class GuiaController : ControllerBase
    {
        public IDiagnosticContext _diagnosticContext { get; }
        
        private ILogger<GuiaController> _logger;
        private readonly IMapper _mapper;

        private readonly IGuiaRepository _GuiaRepository;
        private readonly IUnitOfWork _uow;

        public GuiaController(ILogger<GuiaController> logger, IDiagnosticContext diagnosticContext, [FromServices] IGuiaRepository GuiaRepository, 
            [FromServices] IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
            _GuiaRepository = GuiaRepository;
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

                _GuiaRepository.Save(guia);
                await _uow.CommitAsync();

                return Created($"/v1/guia/{guia.Id}", null);
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
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
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }
    }
}