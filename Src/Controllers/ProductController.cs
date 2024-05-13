using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.Service.Interfaces;
using TallerWeb.Src.DTOs.Product;
using Microsoft.AspNetCore.Authorization;

namespace TallerWeb.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> GetProducts()
        {
            var productsDto = await _service.GetProducts();
            return Ok(productsDto);
        }

        [HttpGet("available")]
        //[Authorize(Roles="Cliente")]
        public async Task<IActionResult> GetProductsAvailable()
        {
            var productDto = await _service.GetProductsAvailable();
            return Ok(productDto);
        }

        [HttpPost("create")]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {   
            var result = await _service.CreateProduct(productDto);
            if(!result){
                return BadRequest("El producto ya existe");
            }
            return Ok("El producto se creó correctamente");
        }

        [HttpPut("{id}")]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var result = await _service.UpdateProduct(id, productUpdateDto);
            if(!result){
                return NotFound("El producto no existe en el sistema.");
            }
            return Ok("El producto se editó correctamente");
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProduct(id);
            if(!result){
                return NotFound("El producto no existe en el sistema.");
            }
            return Ok("El producto se elimino correctamente");
        }

        [HttpPost("buy")]
        //[Authorize(Roles="Cliente")]
        public async Task<IActionResult> BuyProduct(ProductBuyDto productBuyDto)
        {
            var result = await _service.BuyProduct(productBuyDto);
            if(result == null){
                return BadRequest("No hay suficiente stock del producto.");
            }
            return Ok(result);
        }

        [HttpGet("receipt")]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> GetReceipt()
        {
            var receiptsDto = await _service.GetReceipts();
            return Ok(receiptsDto);
        }
    }
}