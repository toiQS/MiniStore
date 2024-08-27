using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.receiptDetail;
using MiniStore.Models;
using MiniStore.Services.receiptDetail;
using System.Diagnostics;
using System.Xml;

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
        [HttpGet]
        public async Task<IActionResult> GetReceiptDetailsAsync()
        {
            var data = await _receiptDetailServices.GetReceiptDetailsAsync();
            if (data == null)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceiptDetailAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(ServiceResult<ReceiptDetailModelResponse>.FailedResult("Id was null"));
            }
            var data = await _receiptDetailServices.GetReceiptDetailAsync(id);
            if (data == null) return Ok(ServiceResult<ReceiptDetailModelResponse>.FailedResult("Data was null or not found"));
            var result = new ReceiptDetailModelResponse()
            {
                ItemId = data.ItemId,
                Quantity = data.Quantity,
                ReceiptDetailId= data.ReceiptDetailId,
                ReceiptId= data.ReceiptId,
            };
            return Ok(ServiceResult<ReceiptDetailModelResponse>.SuccessResult(result));
        }
        [HttpPost]
        public async Task<IActionResult> AddReceipt(ReceiptDetailModelRequest receiptDetail)
        {
            if (receiptDetail == null || ModelState.IsValid) return Ok(ServiceResult<ReceiptDetailModelRequest>.FailedResult("data was invalid or null"));
            var data = new ReceiptDetail()
            {
                ReceiptDetailId = $"RD{DateTime.Now}",
                ItemId= receiptDetail.ItemId,
                Quantity= receiptDetail.Quantity,
                ReceiptId = receiptDetail.ReceiptId,
            };
            var result = await _receiptDetailServices.AddReceipt(data);
            if(result) return Ok(ServiceResult<ReceiptDetailModelRequest>.SuccessResult(receiptDetail));
            return Ok(ServiceResult<bool>.FailedResult("Can't add data"));
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveReceipt(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(ServiceResult<ReceiptDetailModelRequest>.FailedResult("Id was null"));
            }
            var result = await _receiptDetailServices.RemoveReceipt(id);
            if (result) return Ok(ServiceResult<bool>.SuccessResult(result));
            return Ok(ServiceResult<bool>.FailedResult("Can't delete data"));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReceipt(string id, int quantity)
        {
            if (string.IsNullOrEmpty(id) || quantity == 0 || ModelState.IsValid)
            {
                return Ok(ServiceResult<ReceiptDetailModelResponse>.FailedResult("Id was null"));
            }
            var result = await _receiptDetailServices.UpdateReceipt(id, quantity);
            if (result) return Ok(ServiceResult<bool>.SuccessResult(result));
            return Ok(ServiceResult<bool>.FailedResult("Can't update data")); 

        }
    }
}
