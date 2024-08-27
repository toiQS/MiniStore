using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.receiptDetail;
using MiniStore.Models;
using MiniStore.Services.receiptDetail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptDetailController : ControllerBase
    {
        private readonly IReceiptDetailServices _receiptDetailServices;

        public ReceiptDetailController(IReceiptDetailServices receiptDetailServices)
        {
            _receiptDetailServices = receiptDetailServices;
        }

        // Retrieves all receipt details asynchronously
        [HttpGet]
        public async Task<IActionResult> GetReceiptDetailsAsync()
        {
            var data = await _receiptDetailServices.GetReceiptDetailsAsync();
            if (data == null || !data.Any())
            {
                return Ok(ServiceResult<IEnumerable<ReceiptDetailModelResponse>>.FailedResult("Data was null or not found"));
            }

            var result = data.Select(x => new ReceiptDetailModelResponse()
            {
                ItemId = x.ItemId,
                Quantity = x.Quantity,
                ReceiptDetailId = x.ReceiptDetailId,
                ReceiptId = x.ReceiptId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<ReceiptDetailModelResponse>>.SuccessResult(result));
        }

        // Retrieves a specific receipt detail by its ID asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiptDetailAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(ServiceResult<ReceiptDetailModelResponse>.FailedResult("Id was null or empty"));
            }

            var data = await _receiptDetailServices.GetReceiptDetailAsync(id);
            if (data == null)
            {
                return NotFound(ServiceResult<ReceiptDetailModelResponse>.FailedResult("Data was null or not found"));
            }

            var result = new ReceiptDetailModelResponse()
            {
                ItemId = data.ItemId,
                Quantity = data.Quantity,
                ReceiptDetailId = data.ReceiptDetailId,
                ReceiptId = data.ReceiptId,
            };

            return Ok(ServiceResult<ReceiptDetailModelResponse>.SuccessResult(result));
        }

        // Adds a new receipt detail asynchronously
        [HttpPost]
        public async Task<IActionResult> AddReceipt(ReceiptDetailModelRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ServiceResult<ReceiptDetailModelRequest>.FailedResult("Data was invalid or null"));
            }

            var newReceiptDetail = new ReceiptDetail()
            {
                ReceiptDetailId = $"RD{DateTime.Now:yyyyMMddHHmmss}", // Unique ID with timestamp
                ItemId = request.ItemId,
                Quantity = request.Quantity,
                ReceiptId = request.ReceiptId,
            };

            var result = await _receiptDetailServices.AddReceipt(newReceiptDetail);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<ReceiptDetailModelRequest>.FailedResult("Can't add data"));
            }

            return CreatedAtAction(nameof(GetReceiptDetailAsync), new { id = newReceiptDetail.ReceiptDetailId }, request);
        }

        // Removes a receipt detail by its ID asynchronously
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveReceipt(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(ServiceResult<bool>.FailedResult("Id was null or empty"));
            }

            var result = await _receiptDetailServices.RemoveReceipt(id);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<bool>.FailedResult("Can't delete data"));
            }

            return NoContent();
        }

        // Updates an existing receipt detail by its ID asynchronously
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceipt(string id, [FromBody] ReceiptDetailModelRequest request)
        {
            if (string.IsNullOrEmpty(id) || request == null || !ModelState.IsValid)
            {
                return BadRequest(ServiceResult<bool>.FailedResult("Id was null, data was invalid or model state is not valid"));
            }

            var result = await _receiptDetailServices.UpdateReceipt(id, request.Quantity);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<bool>.FailedResult("Can't update data"));
            }

            return NoContent();
        }
    }
}
