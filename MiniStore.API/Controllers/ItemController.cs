using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic;
using MiniStore.API.Models;
using MiniStore.API.Models.item;
using MiniStore.Models;
using MiniStore.Services.item;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemServices _itemServices;
        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetItemsAsync()
        {
            var data = await _itemServices.GetItemsAsync();
            if (data == null) return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Cant't found data or data was null"));
            var result = data.Select(data => new ItemModelResponse()
            {
                ItemId = data.ItemId,
                ItemName = data.ItemName,
                Quantity = data.Quantity,
                Status = data.Status,
                StyleItemId = data.StyleItemId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));
        }
        [HttpGet("status=true")]
        public async Task<IActionResult> GetItemsStatusIsTrueAsync()
        {
            var data = await _itemServices.GetItemsStatusIsTrueAsync();
            if (data == null) return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Cant't found data or data was null"));
            var result = data.Select(data => new ItemModelResponse()
            {
                ItemId = data.ItemId,
                ItemName = data.ItemName,
                Quantity = data.Quantity,
                Status = data.Status,
                StyleItemId = data.StyleItemId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));

        }
        [HttpGet("status=false")]
        public async Task<IActionResult> GetItemsStatusIsFalseAsync()
        {
            var data = await _itemServices.GetItemsStatusIsFalseAsync();
            if (data == null) return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Cant't found data or data was null"));
            var result = data.Select(data => new ItemModelResponse()
            {
                ItemId = data.ItemId,
                ItemName = data.ItemName,
                Quantity = data.Quantity,
                Status = data.Status,
                StyleItemId = data.StyleItemId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return Ok(ServiceResult<ItemModelResponse>.FailedResult("id was null"));
            var data = await _itemServices.GetItemAsync(id);
            if (data == null) return Ok(ServiceResult<ItemModelResponse>.FailedResult("Cant't found data or data was null"));
            var result = new ItemModelResponse()
            {
                ItemId = data.ItemId,
                ItemName = data.ItemName,
                Quantity = data.Quantity,
                Status = data.Status,
                StyleItemId = data.StyleItemId,
            };
            return Ok(ServiceResult<ItemModelResponse>.SuccessResult(result));
        }
        [HttpGet("seach/{text}")]
        public async Task<IActionResult> GetItemsAsyncByText(string text)
        {
            if (string.IsNullOrEmpty(text)) return Ok(ServiceResult<ItemModelResponse>.FailedResult("text was null"));
            var data = await _itemServices.GetItemsAsyncByText(text);
            if (data == null) return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Cant't found data or data was null"));
            var result = data.Select(x => new ItemModelResponse()
            {
                ItemId = x.ItemId,
                ItemName = x.ItemName,
                Quantity = x.Quantity,
                Status = x.Status,
                StyleItemId = x.StyleItemId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));
        }
        [HttpPost]
        public async Task<IActionResult> Add(ItemModelRequest item)
        {
            if (item == null || ModelState.IsValid) return Ok(ServiceResult<bool>.FailedResult("data was null or invalid"));
            var data = new Item()
            {
                ItemId = $"I{DateTime.Now}",
                ItemName = item.ItemName,
                Quantity = 0,
                Status = true,
                StyleItemId = item.StyleItemId,
            };
            var result = await _itemServices.Add(data);
            if(result) return Ok(ServiceResult<ItemModelRequest>.SuccessResult(item));
            return Ok(ServiceResult<bool>.FailedResult("Can't add data"));
        }
        [HttpPut("update status")]
        public async Task<IActionResult> UpdateStatus(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) return Ok(ServiceResult<ItemModelResponse>.FailedResult("id was null"));
            var result = await _itemServices.UpdateStatus(itemId);
            if (result) return Ok(ServiceResult<bool>.SuccessResult(true));
            return Ok(ServiceResult<bool>.FailedResult("Can't update status"));
        }
        [HttpPut]
        public async Task<IActionResult> Update(string itemId, string itemName, string styleItemId)
        {
            if (string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(styleItemId)) return Ok(ServiceResult<ItemModelResponse>.FailedResult("valuse input was null"));
            var result = await _itemServices.Update(itemId, itemName, styleItemId);
            if (result) return Ok(ServiceResult<bool>.SuccessResult(true));
            return Ok(ServiceResult<bool>.FailedResult("Can't update status"));
        }
    }
}
