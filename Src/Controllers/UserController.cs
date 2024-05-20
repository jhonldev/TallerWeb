using System.Security.Claims;
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

        /// <summary>
        /// Obtiene una lista de usuarios basada en una consulta de búsqueda.
        /// </summary>
        /// <param name="query">La consulta de búsqueda utilizada para filtrar los usuarios. Puede ser null o vacía, en cuyo caso se devolverán todos los usuarios.</param>
        /// <returns>Una acción que resulta en una respuesta HTTP. Si la operación es exitosa, la respuesta será Ok (200) 
        /// e incluirá una lista de usuarios. Si ocurre un error, la respuesta será un código de error 500 (Internal Server Error).</returns>

        [HttpGet("findUsers")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult> FindUsers([FromQuery] string query)
        {
            var users = await _service.FindUsers(query);
            return Ok(users);
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns>Una acción que resulta en una respuesta HTTP. Si la operación es exitosa, 
        /// la respuesta será Ok (200) y contendrá los datos de todos los usuarios</returns>
        [HttpGet("getUsers")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<User>> GetUser()
        {
            var user = await _service.GetUsers();
            return Ok(user);
        }
        /// <summary>
        /// Edita los datos del usuario como el nombre, fecha de nacimiento y/o género.
        /// </summary>
        /// <param name="id">El ID del usuario que se va a editar.</param>
        /// <param name="editUserDto">Un objeto que contiene los nuevos datos del usuario.</param>
        /// <returns>Una acción que resulta en una respuesta HTTP. Si la operación es exitosa, la respuesta será Ok (200) 
        /// con un mensaje de éxito. Si el usuario no existe, la respuesta será NotFound (404) </returns>
  
        [HttpPut("editUser")]
        [Authorize]
        public async Task<ActionResult<User>> EditUser(EditUserDto editUserDto){
            var userId = User.FindFirstValue("Id");

            if(userId == null){
                return NotFound("El usuario no existe");
            }
            int id = int.Parse(userId);

            await _service.EditUser(id, editUserDto);
            return Ok("Usuario editado correctamente");

        }

        /// <summary>
        /// Cambia la contraseña de un usuario existente.
        /// </summary>
        /// <param name="id">El ID del usuario cuya contraseña se va a cambiar.</param>
        /// <param name="changePasswordDto">Un objeto que contiene la nueva contraseña del usuario.</param>
        /// <returns>Una acción que resulta en una respuesta HTTP. Si la operación es exitosa, la respuesta será Ok (200) 
        /// con un mensaje de éxito. Si ocurre un problema, la respuesta será BadRequest (400) </returns>
    
        [HttpPut("changePassword")]
        [Authorize]
        public async Task<ActionResult<User>> ChangePassword(ChangePasswordDto changePasswordDto){
            var userId = User.FindFirstValue("Id");
    
            if(userId == null){
                return BadRequest("Hubo un problema en el sistema");
            }
            int id = int.Parse(userId);
            if(!await _service.ChangePassword(id, changePasswordDto))
            {
                return BadRequest("Hubo un problema en el sistema");
            }
            return Ok("Contraseña cambiada correctamente");
        }

        /// <summary>
        /// Habilita o deshabilita a un usuario.
        /// </summary>
        /// <param name="id">El ID del usuario que se va a deshabilitar o habilitar.</param>
        /// <returns>Una acción que resulta en una respuesta HTTP. Si la operación es exitosa, la respuesta será Ok(200)
        /// con un mensaje de éxito. Si el usuario no existe, la respuesta será NotFound (404) </returns>

        [HttpPut("changeActivity/{id}")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<User>> DeleteUser(int id){
            
            var user = await _service.DeleteUser(id);
            if(!user){
                return NotFound("El usuario no existe");
            }
            return Ok("Actividad del usuario cambiada correctamente");
        }
    } 
}
