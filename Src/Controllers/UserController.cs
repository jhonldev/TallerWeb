using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.DTOs.User;
using TallerWeb.Src.Models;
using TallerWeb.Src.Service.Interfaces;

namespace TallerWeb.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult> GetUsers([FromQuery] string query)
        {
            var users = await _service.FindUsers(query);
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> EditUser(int id, EditUserDto editUserDto){
            
            var user = await _service.EditUser(id, editUserDto);
            if(!user){
                return NotFound("El usuario no existe");
            }
            return Ok("Usuario editado correctamente");

        }

        [HttpPut("changePassword/{id}")]
        public async Task<ActionResult<User>> ChangePassword(int id, ChangePasswordDto changePasswordDto){
             var user = await _service.ChangePassword(id, changePasswordDto);
                if(!user){
                    return BadRequest("Hubo un problema en el sistema");
                }
                return Ok("Contraseña cambiada correctamente");
        }
        [HttpPut("changeActivity/{id}")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<User>> DeleteUser(int id){
            
            var user = await _service.DeleteUser(id);
            if(!user){
                return NotFound("El usuario no existe");
            }
            return Ok("Usuario eliminado correctamente");
        }
    } 
}
