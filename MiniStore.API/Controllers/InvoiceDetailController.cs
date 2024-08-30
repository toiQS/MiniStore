using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.invoiceDetail;
using MiniStore.Services.invoiceDetail;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _invoiceService;

        // Constructor to inject dependencies
        public InvoiceDetailController(IInvoiceDetailService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // Get all invoice details
        [HttpGet]
        public async Task<IActionResult> GetInvoiceDetailAsync()
        {
            var data = await _invoiceService.GetInvoiceDetailAsync();
            if (data == null)
            {
                return NotFound(ServiceResult<string>.FailedResult("Not found"));
            }

            // Transform data into response model
            var result = data.Select(x => new InvoiceDetailModelResponse()
            {
                InvoiceDetailId = x.InvoiceDetailId,
                InvoiceId = x.InvoiceId,
                ItemId = x.ItemId,
                Quantity = x.Quantity,
            });

            return Ok(ServiceResult<IEnumerable<InvoiceDetailModelResponse>>.SuccessResult(result));
        }

        // Get invoice details by invoice ID
        [HttpGet("by-invoice/{invoiceId}")]
        public async Task<IActionResult> GetInvoiceDetailByInvoiceIdAsync(string invoiceId)
        {
            if (string.IsNullOrEmpty(invoiceId))
                return BadRequest(ServiceResult<string>.FailedResult("Id was null or invalid"));

            var data = await _invoiceService.GetInvoiceDetailByInvoiceIdAsync(invoiceId);
            if (data == null)
                return NotFound(ServiceResult<string>.FailedResult("Not found"));

            // Transform data into response model
            var result = data.Select(x => new InvoiceDetailModelResponse()
            {
                InvoiceDetailId = x.InvoiceDetailId,
                InvoiceId = x.InvoiceId,
                ItemId = x.ItemId,
                Quantity = x.Quantity,
            });

            return Ok(ServiceResult<IEnumerable<InvoiceDetailModelResponse>>.SuccessResult(result));
        }

        // Get invoice detail by detail ID
        [HttpGet("by-detail/{invoiceDetailId}")]
        public async Task<IActionResult> GetInvoiceDetailByInvoiceDetailIdAsync(string invoiceDetailId)
        {
            if (string.IsNullOrEmpty(invoiceDetailId))
                return BadRequest(ServiceResult<string>.FailedResult("Id was null or invalid"));

            var data = await _invoiceService.GetInvoiceDetailByInvoiceDetailIdAsync(invoiceDetailId);
            if (data == null)
                return NotFound(ServiceResult<string>.FailedResult("Not found"));

            // Transform data into response model
            var result = new InvoiceDetailModelResponse()
            {
                InvoiceDetailId = data.InvoiceDetailId,
                InvoiceId = data.InvoiceId,
                ItemId = data.ItemId,
                Quantity = data.Quantity,
            };

            return Ok(ServiceResult<InvoiceDetailModelResponse>.SuccessResult(result));
        }

        // Add a new invoice detail
        [HttpPost]
        public async Task<IActionResult> Add(string invoiceId, string itemId, int quantity)
        {
            if (string.IsNullOrEmpty(invoiceId) || string.IsNullOrEmpty(itemId) || quantity <= 0)
                return BadRequest(ServiceResult<string>.FailedResult("Data input was null or invalid"));

            var result = await _invoiceService.Add(invoiceId, itemId, quantity);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Add data success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't add new invoice detail"));
        }

        // Update an existing invoice detail
        [HttpPut("{invoiceId}")]
        public async Task<IActionResult> Update(string invoiceId, int quantity)
        {
            if (string.IsNullOrEmpty(invoiceId) || quantity <= 0)
                return BadRequest(ServiceResult<string>.FailedResult("Data input was null or invalid"));

            var result = await _invoiceService.Update(invoiceId, quantity);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't update invoice detail"));
        }

        // Delete an invoice detail
        [HttpDelete("{invoiceId}")]
        public async Task<IActionResult> Delete(string invoiceId)
        {
            if (string.IsNullOrEmpty(invoiceId))
                return BadRequest(ServiceResult<string>.FailedResult("Data input was null or invalid"));

            var result = await _invoiceService.Delete(invoiceId);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't delete invoice detail"));
        }
    }
}
