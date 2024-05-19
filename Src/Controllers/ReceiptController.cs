using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.Service.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TallerWeb.Src.Controllers
{
    /// <summary>
    /// Clase Controller para gestionar los recibos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _service;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ReceiptController"/>.
        /// </summary>
        /// <param name="service">Servicio de recibos.</param>
        public ReceiptController(IReceiptService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los recibos.
        /// </summary>
        /// <returns>Un <see cref="IActionResult"/> que representa los recibos.</returns>
        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> GetReceipt()
        {
            var receiptsDto = await _service.GetReceipts();
            return Ok(receiptsDto);
        }

        /// <summary>
        /// Obtiene los recibos de un cliente específico.
        /// </summary>
        /// <returns>Un <see cref="IActionResult"/> que representa los recibos del cliente.</returns>
        [HttpGet("client")]
        [Authorize(Roles="Cliente")]
        public async Task<IActionResult> GetReceiptByClient()
        {
            var userId = User.FindFirstValue("Id");
            if (userId == null)
            {
                return BadRequest("No se encontró el usuario");
            }

            int id = int.Parse(userId);

            var receiptsDto = await _service.GetReceiptsByClient(id);
            return Ok(receiptsDto);
        }
    }
}