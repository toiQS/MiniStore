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
        private IRepository<Item> _repository;
        private string _path;
        public ItemServices(ApplicationDbContext context, IRepository<Item> repository, string path)
        {
            _context = context;
            _repository = repository;
            _path = _repository.GetPath("item", "LogItemService.txt");
        }
        public ItemServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = new Repository<Item>(_context);
            _path = _repository.GetPath("item", "LogItemService.txt");
        }
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var data = await _repository.GetAll();
            return data;
        }
        public async Task<IEnumerable<Item>> GetItemsStatusIsTrueAsync()
        {
            try
            {
                var data = await _context.Item.AsNoTracking().Where(x => x.Status == true).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }
        public async Task<IEnumerable<Item>> GetItemsStatusIsFalseAsync()
        {
            try
            {
                var data = await _context.Item.AsNoTracking().Where(x => x.Status == false).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }

        public async Task<Item> GetItemAsync(string id)
        {
            try
            {
                var data = await _context.Item.AsNoTracking().FirstOrDefaultAsync(x => x.ItemId == id);
                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }
        public async Task<IEnumerable<Item>> GetItemsAsyncByText(string text)
        {
            try
            {
                var data = await _context.Item.AsNoTracking().Where(x => x.ItemName.ToLower().Contains(text.ToLower())).ToListAsync();
                return data;
            }
            catch(Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }
        public async Task<bool> Add(Item item)
        {
            return await _repository.Add(item);
        }

        public async Task<bool> UpdateStatus(string itemId)
        {
            var data = await _context.Item.AsTracking().FirstOrDefaultAsync(x => x.ItemId == itemId);
            data.Status = !data.Status;
            return await _repository.Update(data);
        }
        public async Task<bool> Update(string itemId, string itemName, string styleItemId)
        {
            var data = await _context.Item.AsTracking().FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (data == null) return false;
            data.ItemName = itemName;
            data.StyleItemId = styleItemId;
            return await _repository.Update(data);
        }
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
