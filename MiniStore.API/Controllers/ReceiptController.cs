using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStore.Data;
using MiniStore.Services.receipt;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptServices _receiptServices;
        public ReceiptController(IReceiptServices receiptServices)
        {
            _receiptServices = receiptServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetReceiptAsync()
        {
            var result = await _receiptServices.GetReceiptsAsync();
            return Ok(result);
        }
    }
}
