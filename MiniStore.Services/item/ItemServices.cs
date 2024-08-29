using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.item;
using MiniStore.Services.repository;
using MiniStore.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.item
{
    public class ItemServices : IItemServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Item> _repository;
        private readonly string _path;

        // Constructor for Unit Test - allows injecting a mock repository and context.
        public ItemServices(ApplicationDbContext context, IRepository<Item> repository, string path)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _path = _repository.GetPath("item", "LogItemService.txt");
        }

        // Constructor for API Controller - uses the default repository implementation.
        public ItemServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = new Repository<Item>(_context);
            _path = _repository.GetPath("item", "LogItemService.txt");
        }

        // Fetches all items from the database.
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _repository.GetAll();
        }

        // Fetches all items with Status = true (active items).
        public async Task<IEnumerable<Item>> GetItemsStatusIsTrueAsync()
        {
            try
            {
                return await _context.Item
                                     .AsNoTracking()
                                     .Where(x => x.Status == true)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }

        // Fetches all items with Status = false (inactive items).
        public async Task<IEnumerable<Item>> GetItemsStatusIsFalseAsync()
        {
            try
            {
                return await _context.Item
                                     .AsNoTracking()
                                     .Where(x => x.Status == false)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }

        // Fetches a single item by its ID.
        public async Task<Item> GetItemAsync(string id)
        {
            try
            {
                return await _context.Item
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(x => x.ItemId == id);
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }

        // Fetches items by a search text (searches in item name).
        public async Task<IEnumerable<Item>> GetItemsAsyncByText(string text)
        {
            try
            {
                return await _context.Item
                                     .AsNoTracking()
                                     .Where(x => x.ItemName.ToLower().Contains(text.ToLower()))
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }

        // Adds a new item to the database.
        public async Task<bool> AddNewItem(string itemName, int quantity, string styleItemId, string supplierId)
        {
            var item = new Item()
            {
                ItemId = $"I{DateTime.Now:yyyyMMddHHmmssfff}", // Ensure uniqueness by using a timestamp.
                StyleItemId = styleItemId,
                SupplierId = supplierId,
                ItemName = itemName,
                Quantity = quantity,
                Status = true
            };
            return await _repository.Add(item);
        }

        // Updates the quantity of an existing item.
        public async Task<bool> UpdateItem(string itemId, int quantity)
        {
            var data = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (data == null) return false;
            data.Quantity += quantity;
            return await _repository.Update(data);
        }

        // Toggles the status of an item by its ID.
        public async Task<bool> UpdateStatusItem(string itemId)
        {
            var data = await _context.Item
                                     .AsTracking()
                                     .FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (data == null) return false;

            data.Status = !data.Status;
            return await _repository.Update(data);
        }

        // Updates item details.
        public async Task<bool> UpdateInfoItem(string itemId, string itemName, string styleItemId, string supplierId)
        {
            var data = await _context.Item
                                     .AsTracking()
                                     .FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (data == null) return false;

            data.ItemName = itemName;
            data.StyleItemId = styleItemId;
            data.SupplierId = supplierId;
            return await _repository.Update(data);
        }

        // Deletes an item from the database.
        public async Task<bool> DeleteItemAsync(string itemId)
        {
            var data = await _context.Item
                                      .AsTracking()
                                      .FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (data == null) return false;
            return await _repository.Delete(data);
        }

        public async Task<bool> Sell(string itemId, int quantity)
        {
            var data = await _context.Item
                                     .AsTracking()
                                     .FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (data == null) return false;
            data.Quantity -= quantity;
            return await _repository.Update(data);
        }
        // Logs error details to a file asynchronously.
        private async Task LogErrorAsync(Exception ex)
        {
            var errorDetails = new StringBuilder();
            errorDetails.AppendLine($"Message: {ex.Message}");
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}");
            errorDetails.AppendLine($"Source: {ex.Source}");
            errorDetails.AppendLine($"Time: {DateTime.Now}");

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString());
            }
        }
    }
}
