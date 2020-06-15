using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Dtos;
using Repository.Interfaces;
using Backend.Arguments;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;
using Serilog;
using System.Threading;

namespace Backend.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        static int _callCount;
        private ILogger<StudentController> _logger;
        public IDiagnosticContext _diagnosticContext { get; }
        private readonly IMapper _mapper;

        private readonly IStudentRepository _StudentRepository;
        private readonly IUnitOfWork _uow;
        

        public StudentController(ILogger<StudentController> logger, IDiagnosticContext diagnosticContext, [FromServices] IStudentRepository studentRepository, 
            [FromServices] IUnitOfWork uow,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));

            _StudentRepository = studentRepository;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("v1/student")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]StudentDto studentDto)
        {
            _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));
            
            var validator = new StudentValidator();
            var results = validator.Validate(studentDto);

            // Request Validation
            if(!results.IsValid) {
                var erros = new List<string>();

                foreach(var failure in results.Errors) {
                    erros.Add($"Property {failure.PropertyName} failed validation. Error was: {failure.ErrorMessage}");
                }

                return this.StatusCode(StatusCodes.Status400BadRequest, erros);
            }

            // Domain validation
            var student = _mapper.Map<StudentDto, Student>(studentDto);
            if(student.Email.Invalid)
            {
                var erros = new List<string>();

                foreach (var notification in student.Notifications)
                    erros.Add($"Property {notification.Property} failed domain validation. Error was: {notification.Message}");
                
                return this.StatusCode(StatusCodes.Status400BadRequest, erros);
            }

            try
            {
                _StudentRepository.Save(student);
                
                await _uow.CommitAsync();
                return Created($"/v1/student/{studentDto.Id}", _mapper.Map<StudentDto>(studentDto));
            }
            catch (System.Exception ex)
            {
                _uow.Rollback();
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("v1/student/{IdGuiaExterno}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id){
            try
            {
                _StudentRepository.Delete(id);
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
        [Route("v1/student")]
        //[Authorize (AuthenticationSchemes = "Bearer")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var students = await _StudentRepository.All();
                var studentDto = _mapper.Map<StudentDto[]>(students);

                //return base.Ok(); 
                return Ok(studentDto);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no banco de dados, detalhes : {ex.Message}");
            }
        }

        [HttpGet]
        [Route("v1/student/{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var student = await _StudentRepository.GetByIdAsync(id);

                var result = _mapper.Map<StudentDto>(student);

                return base.Ok(result); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados");
            }
        }
    }
}