
using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.DTOs.User;
using TallerWeb.Src.Service.Interfaces;

namespace TallerWeb.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser(LoginUserDto loginUserDto)
        {
            var respuesta = await _authService.LoginUser(loginUserDto);
            if (respuesta is null)
            {
                return BadRequest("Credenciales invalidas");
            }

            return Ok(respuesta);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var respuesta = await _authService.RegisterUser(registerUserDto);

            if (respuesta != null)
            {
                return BadRequest (respuesta);
            }
            return Ok(respuesta);
        }
        [HttpPost("logout")]
        public ActionResult<string> LogoutUser()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == "Id")?.Value;
            if (userId is null)
            {
                return BadRequest("Usuario no logueado");
            }
            return Ok("Usuario deslogueado correctamente");
        }
    }
}