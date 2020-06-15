using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Backend.Filters
{
    public class CustomExceptionFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = 500;
            //var message = context.Exception is DomainException? context.Exception.Message : "An error ocorred";
            var message = context.Exception.Message;
            
            _logger.LogError(message);
            context.Result = new JsonResult(message);
            context.ExceptionHandled = true;

            base.OnException(context);
        }
    }
}