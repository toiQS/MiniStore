using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.styleItem;
using MiniStore.Models;
using MiniStore.Services.styleItem;

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

        // GET: api/StyleItem
        [HttpGet]
        public async Task<IActionResult> GetStyleItemsAsync()
        {
            var data = await _styleItemService.GetStyleItemsAsync();
            if (data == null)
                return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.FailedResult("Data was null"));

            var result = data.Select(x => new StyleItemModelResponse
            {
                StyleItemName = x.StyleItemName,
                Status = x.Status,
                StyleItemId = x.StyleItemId
            }).ToList();

            return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.SuccessResult(result));
        }

        // GET: api/StyleItem/search/{text}
        [HttpGet("search/{text}")]
        public async Task<IActionResult> GetStyleItemsByTextAsync(string text)
        {
            if (string.IsNullOrEmpty(text))
                return BadRequest(ServiceResult<bool>.FailedResult("Text was null"));

            var data = await _styleItemService.GetStyleItemsByTextAsync(text);
            if (data == null)
                return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.FailedResult("Data was null"));

            var result = data.Select(x => new StyleItemModelResponse
            {
                StyleItemName = x.StyleItemName,
                Status = x.Status,
                StyleItemId = x.StyleItemId
            }).ToList();

            return Ok(ServiceResult<IEnumerable<StyleItemModelResponse>>.SuccessResult(result));
        }

        // GET: api/StyleItem/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStyleItemByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(ServiceResult<bool>.FailedResult("Id was null"));

            var data = await _styleItemService.GetStyleItemByIdAsync(id);
            if (data == null)
                return Ok(ServiceResult<StyleItemModelResponse>.FailedResult("Data was null"));

            var result = new StyleItemModelResponse
            {
                StyleItemName = data.StyleItemName,
                Status = data.Status,
                StyleItemId = data.StyleItemId
            };

            return Ok(ServiceResult<StyleItemModelResponse>.SuccessResult(result));
        }

        // POST: api/StyleItem
        [HttpPost]
        public async Task<IActionResult> Add(string styleItemName, string styleItemDescription)
        {
            if (string.IsNullOrEmpty(styleItemName) || string.IsNullOrEmpty(styleItemDescription) || !ModelState.IsValid)
                return BadRequest(ServiceResult<bool>.FailedResult("Data was invalid or null"));

            var result = await _styleItemService.Add(styleItemName, styleItemDescription);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Add style item success"));

            return BadRequest(ServiceResult<bool>.FailedResult("Can't add data"));
        }

        // DELETE: api/StyleItem
        [HttpDelete]
        public async Task<IActionResult> Remove(string styleItemId)
        {
            if (string.IsNullOrEmpty(styleItemId))
                return BadRequest(ServiceResult<bool>.FailedResult("Id was null"));

            var result = await _styleItemService.Remove(styleItemId);
            if (result)
                return NoContent();

            return BadRequest(ServiceResult<bool>.FailedResult("Can't remove data"));
        }

        // PUT: api/StyleItem
        [HttpPut]
        public async Task<IActionResult> Update(string styleItemId, string styleItemDescription, string styleItemName)
        {
            if (string.IsNullOrEmpty(styleItemId) || string.IsNullOrEmpty(styleItemName) || string.IsNullOrEmpty(styleItemDescription))
                return BadRequest(ServiceResult<bool>.FailedResult("Data was null"));

            var result = await _styleItemService.Update(styleItemId, styleItemDescription, styleItemName);
            if (result)
                return NoContent();

            return BadRequest(ServiceResult<bool>.FailedResult("Can't update data"));
        }

        // PUT: api/StyleItem/edit-status
        [HttpPut("edit-status")]
        public async Task<IActionResult> UpdateStatus(string styleItemId)
        {
            if (string.IsNullOrEmpty(styleItemId))
                return BadRequest(ServiceResult<bool>.FailedResult("Id was null"));

            var result = await _styleItemService.Update(styleItemId);
            if (result)
                return NoContent();

            return BadRequest(ServiceResult<bool>.FailedResult("Can't update data"));
        }
    }
}
