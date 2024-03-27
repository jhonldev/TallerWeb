using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.Repositories.Interfaces; 

namespace TallerWeb.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _service;

        public UserController(IUserRepository service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetUsers();
            return Ok(users);
        }
    } 
}