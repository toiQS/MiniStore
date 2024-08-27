using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
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

        // Constructor for Unit Test
        public ItemServices(ApplicationDbContext context, IRepository<Item> repository, string path)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _path = _repository.GetPath("item", "LogItemService.txt");
        }

        // Constructor for Api Controller
        public ItemServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = new Repository<Item>(_context);
            _path = _repository.GetPath("item", "LogItemService.txt");
        }

        // Fetch all items from the database
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _repository.GetAll();
        }

        // Fetch all items with Status = true
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

        // Fetch all items with Status = false
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

        // Fetch a single item by its ID
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

        // Fetch items by a search text (searches in item name)
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

        // Add a new item to the database
        public async Task<bool> Add(Item item)
        {
            return await _repository.Add(item);
        }

        // Toggle the status of an item by its ID
        public async Task<bool> UpdateStatus(string itemId)
        {
            var data = await _context.Item
                                     .AsTracking()
                                     .FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (data == null) return false;

            data.Status = !data.Status;
            return await _repository.Update(data);
        }

        // Update item details
        public async Task<bool> Update(string itemId, string itemName, string styleItemId)
        {
            var data = await _context.Item
                                     .AsTracking()
                                     .FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (data == null) return false;

            data.ItemName = itemName;
            data.StyleItemId = styleItemId;
            return await _repository.Update(data);
        }

        // Log error details to a file asynchronously
        private async Task LogErrorAsync(Exception ex)
        {
            var errorDetails = new StringBuilder();
            errorDetails.AppendLine($"Message: {ex.Message}\n");
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
            errorDetails.AppendLine($"Source: {ex.Source}\n");
            errorDetails.AppendLine($"Time: {DateTime.Now}\n");

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString());
            }
        }
    }
}
