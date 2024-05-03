using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.Repositories.Interfaces; 
using TallerWeb.Src.Models;
using Microsoft.AspNetCore.Authorization;

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
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProducts();

            var productDtos = products.Select(product => new ProductDto
            {
                Nombre = product.Nombre,
                Tipo = product.Tipo,
                Precio = product.Precio,
                CantidadEnStock = product.CantidadEnStock,
                Image = product.Image
            }).ToList();

            return Ok(productDtos);
        }

        [HttpPost("create")]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {   
            var result = await _service.ExistingProduct(productDto.Nombre, productDto.Tipo);
            if(result){
                return BadRequest("El producto ya existe");
            }
            var product = new Product
            {
                Nombre = productDto.Nombre,
                Tipo = productDto.Tipo,
                Precio = productDto.Precio,
                CantidadEnStock = productDto.CantidadEnStock,
                Image = productDto.Image
            };

            var newProduct = await _service.CreateProduct(product);

            var newProductDto = new ProductDto
            {
                Nombre = newProduct.Nombre,
                Tipo = newProduct.Tipo,
                Precio = newProduct.Precio,
                CantidadEnStock = newProduct.CantidadEnStock,
                Image = newProduct.Image
            };

            return Ok("El producto se creó correctamente");
        }

        [HttpPut("{id}")]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productDto)
        {
            var productUpdateDto = new ProductUpdateDto
            {
                Nombre = productDto.Nombre,
                Tipo = productDto.Tipo,
                Precio = productDto.Precio,
                CantidadEnStock = productDto.CantidadEnStock,
                Image = productDto.Image
            };

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
    }
}