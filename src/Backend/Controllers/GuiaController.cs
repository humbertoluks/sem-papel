using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Domain.Models;
using Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Serilog;
using System.Threading;

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
        public async Task<IActionResult> Post([FromBody]Guia guia)
        {
            try
            {
                _GuiaRepository.Save(guia);
                await _uow.CommitAsync();

                return Created($"/v1/guia/{guia.Id}", guia);
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("v1/guia/{IdGuiaExterno:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id){
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
        //[Authorize (AuthenticationSchemes = "Bearer")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var guias = await _GuiaRepository.All();

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