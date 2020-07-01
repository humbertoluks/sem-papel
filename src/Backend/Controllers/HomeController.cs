using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Backend.Services;
using Domain.Models.Account;
using Repository;

namespace Backend.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = UserRepository.Get(model.UserName, model.AccessKey);

            if (user ==  null)
                return Task.FromResult(NotFound(new { message = "Usuário ou senha inválidos" }));

            var token = TokenService.GenerateToken(user);
            
            user.AccessKey = "";
            
            var result = new
            {
                user,
                token
            };
            return await Task.FromResult(result);
       }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);
    }
}