using Microsoft.AspNetCore.Mvc;
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

        // Constructor with dependency injection
        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        /// <summary>
        /// Retrieves all items.
        /// </summary>
        /// <returns>A list of items.</returns>
        [HttpGet]
        public async Task<IActionResult> GetItemsAsync()
        {
            var data = await _itemServices.GetItemsAsync();
            if (data == null)
                return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Can't find data or data is null"));

            var result = data.Select(item => new ItemModelResponse()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                Status = item.Status,
                StyleItemId = item.StyleItemId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));
        }

        /// <summary>
        /// Retrieves all items with a status of true.
        /// </summary>
        /// <returns>A list of active items.</returns>
        [HttpGet("status=true")]
        public async Task<IActionResult> GetItemsStatusIsTrueAsync()
        {
            var data = await _itemServices.GetItemsStatusIsTrueAsync();
            if (data == null)
                return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Can't find data or data is null"));

            var result = data.Select(item => new ItemModelResponse()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                Status = item.Status,
                StyleItemId = item.StyleItemId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));
        }

        /// <summary>
        /// Retrieves all items with a status of false.
        /// </summary>
        /// <returns>A list of inactive items.</returns>
        [HttpGet("status=false")]
        public async Task<IActionResult> GetItemsStatusIsFalseAsync()
        {
            var data = await _itemServices.GetItemsStatusIsFalseAsync();
            if (data == null)
                return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Can't find data or data is null"));

            var result = data.Select(item => new ItemModelResponse()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                Status = item.Status,
                StyleItemId = item.StyleItemId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));
        }

        /// <summary>
        /// Retrieves a specific item by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item details.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Ok(ServiceResult<ItemModelResponse>.FailedResult("ID is null"));

            var data = await _itemServices.GetItemAsync(id);
            if (data == null)
                return Ok(ServiceResult<ItemModelResponse>.FailedResult("Can't find data or data is null"));

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

        /// <summary>
        /// Searches for items based on a text query.
        /// </summary>
        /// <param name="text">The text to search for.</param>
        /// <returns>A list of matching items.</returns>
        [HttpGet("search/{text}")]
        public async Task<IActionResult> GetItemsAsyncByText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Ok(ServiceResult<ItemModelResponse>.FailedResult("Text is null"));

            var data = await _itemServices.GetItemsAsyncByText(text);
            if (data == null)
                return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.FailedResult("Can't find data or data is null"));

            var result = data.Select(item => new ItemModelResponse()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                Status = item.Status,
                StyleItemId = item.StyleItemId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<ItemModelResponse>>.SuccessResult(result));
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="itemName">The name of the item.</param>
        /// <param name="quantity">The quantity of the item.</param>
        /// <param name="styleItemId">The style ID of the item.</param>
        /// <param name="supplierId">The supplier ID of the item.</param>
        /// <returns>The result of the addition.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(string itemName, int quantity, string styleItemId, string supplierId)
        {
            if (string.IsNullOrEmpty(itemName) ||
                string.IsNullOrEmpty(supplierId) ||
                string.IsNullOrEmpty(styleItemId) ||
                quantity == 0 ||
                !ModelState.IsValid)
                return Ok(ServiceResult<bool>.FailedResult("Data is null or invalid"));

            var result = await _itemServices.AddNewItem(itemName, quantity, styleItemId, supplierId);
            if (result)
                return Ok(ServiceResult<bool>.SuccessResult(result));

            return Ok(ServiceResult<bool>.FailedResult("Can't add data"));
        }

        /// <summary>
        /// Updates the status of an item.
        /// </summary>
        /// <param name="itemId">The ID of the item to update.</param>
        /// <returns>The result of the update.</returns>
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                return BadRequest(ServiceResult<ItemModelResponse>.FailedResult("ID is null"));

            var result = await _itemServices.UpdateStatusItem(itemId);
            if (result)
                return Ok(ServiceResult<bool>.SuccessResult(true));

            return Ok(ServiceResult<bool>.FailedResult("Can't update status"));
        }

        /// <summary>
        /// Updates an item's details.
        /// </summary>
        /// <param name="itemId">The ID of the item to update.</param>
        /// <param name="itemName">The new name of the item.</param>
        /// <param name="styleItemId">The new style ID of the item.</param>
        /// <param name="supplierId">The new supplier ID of the item.</param>
        /// <returns>The result of the update.</returns>
        [HttpPut]
        public async Task<IActionResult> Update(string itemId, string itemName, string styleItemId, string supplierId)
        {
            if (string.IsNullOrEmpty(itemId) ||
                string.IsNullOrEmpty(itemName) ||
                string.IsNullOrEmpty(styleItemId) ||
                string.IsNullOrEmpty(supplierId))
                return Ok(ServiceResult<ItemModelResponse>.FailedResult("Input values are null"));

            var result = await _itemServices.UpdateInfoItem(itemId, itemName, styleItemId, supplierId);
            if (result)
                return Ok(ServiceResult<bool>.SuccessResult(true));

            return Ok(ServiceResult<bool>.FailedResult("Can't update item details"));
        }

        /// <summary>
        /// Deletes an item by ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to delete.</param>
        /// <returns>The result of the deletion.</returns>
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
                return BadRequest(ServiceResult<ItemModelResponse>.FailedResult("ID is null"));

            var result = await _itemServices.DeleteItemAsync(itemId);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Delete success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't delete data"));
        }
    }
}
