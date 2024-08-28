using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MiniStore.API.Models;
using MiniStore.API.Models.styleItem;
using MiniStore.Models;
using MiniStore.Services.styleItem;
using System.Diagnostics;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleItemController : ControllerBase
    {
        private readonly IStyleItemService _styleItemService;
        public StyleItemController(IStyleItemService styleItemService)
        {
            _styleItemService = styleItemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetStyleItemsAsync()
        {
            var data = await _styleItemService.GetStyleItemsAsync();
            if (data == null) return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.FailedResult("data was null"));
            var result = data.Select(x => new StyleItemModelResponse()
            {
                StyleItemName = x.StyleItemName,
                Status = x.Status,
                StyleItemId = x.StyleItemId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.SuccessResult(result));
        }
        [HttpGet("search/{text}")]
        public async Task<IActionResult> GetStyleItemsByTextAsync(string text)
        {
            if (string.IsNullOrEmpty(text)) return Ok(ServiceResult<bool>.FailedResult("text was nul"));
            var data = await _styleItemService.GetStyleItemsByTextAsync(text);
            if (data == null) return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.FailedResult("data was null"));
            var result = data.Select(x => new StyleItemModelResponse()
            {
                StyleItemName = x.StyleItemName,
                Status = x.Status,
                StyleItemId = x.StyleItemId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.SuccessResult(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStyleItemByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return Ok(ServiceResult<bool>.FailedResult("Id was null"));
            var data =  await _styleItemService.GetStyleItemByIdAsync(id);
            if (data == null) return Ok(ServiceResult<StyleItemModelResponse>.FailedResult("data was null"));
            var result = new StyleItemModelResponse()
            {
                StyleItemName = data.StyleItemName,
                Status = data.Status,
                StyleItemId= data.StyleItemId,
            };
            return Ok(ServiceResult<StyleItemModelResponse>.SuccessResult(result));
        }
        [HttpPost]
        public async Task<IActionResult> Add(StyleItemModelRequest styleItem)
        {
            if (styleItem == null || ModelState.IsValid) return Ok(ServiceResult<bool>.FailedResult("data was invalid or null"));
            var data = new StyleItem()
            {
                StyleItemName = styleItem.StyleItemName,
                Status = true,
                StyleItemDescription = styleItem.StyleItemDescription,
                StyleItemId = $"SI{DateTime.Now}",
            };
            var result = await _styleItemService.Add(data);
            if(result) return Ok(ServiceResult<StyleItemModelRequest>.SuccessResult(styleItem));
            return Ok(ServiceResult<bool>.FailedResult("Can't add data"));

        }
        [HttpDelete]
        public async Task<IActionResult> Remove(string styleItemId)
        {
            if (string.IsNullOrEmpty(styleItemId)) return Ok(ServiceResult<bool>.FailedResult("id was null"));
            var result = await _styleItemService.Remove(styleItemId);
            if (result) return NoContent();
            return Ok(ServiceResult<bool>.FailedResult("Can't remove data"));

        }
        [HttpPut]
        public async Task<IActionResult> Update(string styleItemId, string styleItemDescriptiontyle, string styleItemName)
        {
            if (string.IsNullOrEmpty(styleItemId) || string.IsNullOrEmpty(styleItemName) || string.IsNullOrEmpty(styleItemDescriptiontyle))
            {
                return Ok(ServiceResult<bool>.FailedResult("Data was null"));
            }
            var result = await _styleItemService.Update(styleItemId, styleItemDescriptiontyle, styleItemName);
            if (result) return NoContent();
            return Ok(ServiceResult<bool>.FailedResult("Can't remove data"));
        }
        [HttpPut("edit-status")]
        public async Task<IActionResult> Update(string styleItemId)
        {
            if (string.IsNullOrEmpty(styleItemId)) return Ok(ServiceResult<bool>.FailedResult("id was null"));
            var result = await _styleItemService.Update(styleItemId);
            if (result) return NoContent();
            return Ok(ServiceResult<bool>.FailedResult("Can't remove data"));
        }

    }
}
