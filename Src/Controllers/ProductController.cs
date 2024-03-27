using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.Repositories.Interfaces; 

namespace TallerWeb.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _service;

        public ProductController(IProductRepository service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProducts();
            return Ok(products);
        }
    }
}