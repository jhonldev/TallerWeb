using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TallerWeb.Src.Service.Interfaces;
using TallerWeb.Src.DTOs.Product;
using Microsoft.AspNetCore.Authorization;

namespace TallerWeb.Src.Controllers
{
    /// <summary>
    /// Clase Controller para gestionar los productos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProductController"/>.
        /// </summary>
        /// <param name="service">Service de productos.</param>
        public ProductController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene la lista de productos según la consulta especificada.
        /// </summary>
        /// <param name="query">Cadena de caracteres a consultar.</param>
        /// <returns>Lista de productos.</returns>
        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetProducts([FromQuery] string query = "")
        {
            var productsDto = await _service.GetProducts(query);
            return Ok(productsDto);
        }

        /// <summary>
        /// Obtiene la lista de productos disponibles.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        [HttpGet("available")]
        [Authorize(Roles="Cliente")]
        public async Task<IActionResult> GetProductsAvailable()
        {
            var productDto = await _service.GetProductsAvailable();
            return Ok(productDto);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="productDto">Datos del producto.</param>
        /// <param name="photo">Foto del producto.</param>
        /// <returns>Resultado de la operación de crear.</returns>
        [HttpPost("create")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto,[FromForm] IFormFile photo)
        {   
            var result = await _service.CreateProduct(productDto,photo);
            if(!result){
                return BadRequest("Error al crear el producto.");
            }
            return Ok("El producto se creó correctamente");
        }

        /// <summary>
        /// Actualizar producto.
        /// </summary>
        /// <param name="id">Id del producto a actualizar.</param>
        /// <param name="productUpdateDto">Datos del producto.</param>
        /// <returns>Resultado de la operación de actualizar.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            var result = await _service.UpdateProduct(id, productUpdateDto);
            if(!result){
                return NotFound("El producto no existe en el sistema.");
            }
            return Ok("El producto se editó correctamente");
        }

        /// <summary>
        /// Eliminar producto.
        /// </summary>
        /// <param name="id">Id del producto a actualizar.</param>
        /// <returns>Resultado de la operación de eliminar.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProduct(id);
            if(!result){
                return NotFound("El producto no existe en el sistema.");
            }
            return Ok("El producto se elimino correctamente");
        }

        /// <summary>
        /// Comprar producto.
        /// </summary>
        /// <param name="productBuyDto">Datos del producto.</param>
        /// <returns>Resultado de la operación de comprar.</returns>
        [HttpPost("buy")]
        [Authorize(Roles ="Cliente")]
        public async Task<IActionResult> BuyProduct(ProductBuyDto productBuyDto)
        {
            if (User.FindFirstValue("Id") == null)
            {
                return Unauthorized("No se pudo obtener el ID del usuario.");
            }
            else
            {
                var idUser = User.FindFirstValue("Id");

                if (idUser == null)
                {
                    return Unauthorized("No se pudo obtener el ID del usuario.");
                }

                var result = await _service.BuyProduct(productBuyDto, idUser);
                if(result == null){
                    return BadRequest("Hubo un problema al realizar la compra.");
                }
                return Ok(result);
            }
        }

    }
}