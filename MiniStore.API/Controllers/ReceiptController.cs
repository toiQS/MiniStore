using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.receipt;
using MiniStore.Models;
using MiniStore.Services.receipt;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptServices _receiptServices;

        public ReceiptController(IReceiptServices receiptServices)
        {
            _receiptServices = receiptServices;
        }

        // Retrieves all receipts asynchronously
        [HttpGet]
        public async Task<IActionResult> GetReceiptAsync()
        {
            var result = await _receiptServices.GetReceiptsAsync();
            return Ok(result);
        }

        // Retrieves a specific receipt by its ID asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiptAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(ServiceResult<ReceiptModelResponse>.FailedResult("Id was null"));
            }

            var result = await _receiptServices.GetReceiptAsync(id);

            if (result == null)
            {
                return Ok(ServiceResult<ReceiptModelResponse>.FailedResult("Can't find data"));
            }

            var data = new ReceiptModelResponse()
            {
                ReceiptId = result.ReceiptId,
                SupplierID = result.SupplierID,
                CreateAt = result.CreateAt,
            };

            return Ok(ServiceResult<ReceiptModelResponse>.SuccessResult(data));
        }

        // Retrieves receipts based on a search text asynchronously
        [HttpGet("{text}")]
        public async Task<IActionResult> GetReceiptsByText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Ok(ServiceResult<IEnumerable<ReceiptModelResponse>>.FailedResult("Text was null"));
            }

            var result = await _receiptServices.GetReceiptByText(text);
            if (result == null)
            {
                return Ok(ServiceResult<IEnumerable<ReceiptModelResponse>>.FailedResult("Can't find or data doesn't exist"));
            }

            var data = result.Select(x => new ReceiptModelResponse()
            {
                CreateAt = x.CreateAt,
                ReceiptId = x.ReceiptId,
                SupplierID = x.SupplierID,
            }).ToArray();

            return Ok(ServiceResult<IEnumerable<ReceiptModelResponse>>.SuccessResult(data));
        }

        // Adds a new receipt asynchronously
        [HttpPost]
        public async Task<IActionResult> AddNewReceipt(ReceiptModelRequest request)
        {
            if (ModelState.IsValid)
            {
                var newReceipt = new Receipt()
                {
                    ReceiptId = $"R{DateTime.Now}", // Example Receipt ID generation, consider adjusting to your needs
                    CreateAt = request.CreateAt,
                    SupplierID = request.SupplierID,
                };

                var result = await _receiptServices.Create(newReceipt);
                if (!result)
                {
                    return Ok(ServiceResult<bool>.FailedResult("Can't add data"));
                }

                return Ok(ServiceResult<bool>.SuccessResult(result));
            }

            return Ok(ServiceResult<bool>.FailedResult("Data was invalid"));
        }

        // Removes a receipt by its ID asynchronously
        [HttpDelete]
        public async Task<IActionResult> RemoveReceipt(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return Ok(ServiceResult<bool>.FailedResult("Id was null"));
            }

            var result = await _receiptServices.Remove(Id);
            if (!result)
            {
                return Ok(ServiceResult<bool>.FailedResult("Can't remove data"));
            }

            return Ok(ServiceResult<bool>.SuccessResult(result));
        }

        // Updates an existing receipt by its ID asynchronously
        [HttpPut]
        public async Task<IActionResult> UpdateReceipt(string Id, ReceiptModelRequest request)
        {
            if (string.IsNullOrEmpty(Id) || request == null)
            {
                return Ok(ServiceResult<bool>.FailedResult("Id was null or receipt was invalid"));
            }

            var data = new Receipt()
            {
                CreateAt = request.CreateAt,
                SupplierID = request.SupplierID,
            };

            var result = await _receiptServices.Update(Id, data);
            if (!result)
            {
                return Ok(ServiceResult<bool>.FailedResult("Can't update data"));
            }

            return Ok(ServiceResult<bool>.SuccessResult(result));
        }
    }
}
