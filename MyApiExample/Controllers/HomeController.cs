using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiExample.Models;
using MyApiExample.Services;

namespace MyApiExample.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        protected readonly IUserService _userService;
        protected readonly ITokenService _tokenService;
        public HomeController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpGet]
        [Route("welcome")]
        [AllowAnonymous]
        public string Anonymous() => "Api is running! Welcome.";

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            try
            {
                var user = _userService.Authenticate(model);
                if (user.user != null) user.user.Token = _tokenService.GenerateToken(user.user);

                return user;
            }
            catch (Exception ex)
            {
                var user = new User();
                return new { user, message = string.Format("Erro ao Fazer Login! {0}", ex) };
            }
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> CreateUser([FromBody] User user)
        {
            try
            {
                if (user.Id != 0)
                    return new { user, message = string.Format("Erro ao criar usuário!") };

                var users = _userService.CreateEditUser(user);

                return users;
            }
            catch (Exception ex)
            {
                return new { user, message = string.Format("Erro ao criar usuário! {0}", ex)  };
            }
        }

        [HttpPost]
        [Route("edit")]
        [Authorize]
        public async Task<ActionResult<dynamic>> EditUser([FromBody] User user)
        {
            try
            {
                var users = _userService.CreateEditUser(user);

                return users;
            }
            catch (Exception ex)
            {
                return new { user, message = string.Format("Erro ao Buscar Usuarios! {0}", ex) };
            }
        }

        [HttpPost]
        [Route("users")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetUsers([FromBody]User user)
        {
            try
            {
                var users = _userService.GetUsers(user);

                return users;
            }
            catch (Exception ex)
            {
                return new { user, message = string.Format("Erro ao Buscar Usuarios! {0}", ex) };
            }
        }

        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task<ActionResult<dynamic>> Delete([FromBody] User user)
        {
            try
            {
                var users = _userService.DeleteUsers(user);
                return users;
            }
            catch (Exception ex)
            {
                return new { user, message = string.Format("Erro ao Buscar Usuarios! {0}", ex) };
            }
        }

    }
}
