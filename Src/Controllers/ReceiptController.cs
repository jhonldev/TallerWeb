using Microsoft.AspNetCore.Mvc;
using TallerWeb.Src.Service.Interfaces;
using TallerWeb.Src.DTOs.Receipt;
using Microsoft.AspNetCore.Authorization;

namespace TallerWeb.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _service;

        public ReceiptController(IReceiptService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> GetReceipt()
        {
            var receiptsDto = await _service.GetReceipts();
            return Ok(receiptsDto);
        }

        [HttpGet("/client")]
        //[Authorize(Roles="Admin")]
        public async Task<IActionResult> GetReceiptByClient()
        {
            int id = 1; ///CAMBIAR POR EL OBTENER EL ID DEL USUARIO LOGUEADO

            var receiptsDto = await _service.GetReceiptsByClient(id);
            return Ok(receiptsDto);
        }
        
    }
}