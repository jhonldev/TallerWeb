
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
        
        /// <summary>
        /// Autentica a un usuario y devuelve un token de acceso.
        /// </summary>
        /// <param name="loginUserDto">Un objeto que contiene las credenciales del usuario (correo del ususario y contraseña)</param>
        /// <returns>Una acción que resulta en una respuesta HTTP. Si la operacion es exitosa la respuesta será OK(200)
        /// y tendrá el token de acceso. Si las credenciales son invalidas la respuesta será BadRequest (400)
        /// con un mensaje de error </returns>
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
        /// <summary>
        /// Registra un nuevo ususario y debuelve un token de acceso
        /// </summary>
        /// <param name="registerUserDto">Un objeto que contiene los datos del nuevo usuario </param>
        /// <returns>Una acción que resulta en una respuesta HTTP. Si la operación es exitosa, la respuesta será OK (200)
        /// y contenrá el token de acceso. Si el registro falla, la respuesta será BadRequest (400)</returns>
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
        
    }
}