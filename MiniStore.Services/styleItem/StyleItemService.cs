using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using System.Text;

namespace MiniStore.Services.styleItem
{
    public class StyleItemService : IStyleItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<StyleItem> _repository;
        private readonly string _path;

        // Constructor for API controller
        public StyleItemService(ApplicationDbContext context)
        {
            _context = context;
            _repository = new Repository<StyleItem>(context);
            _path = _repository.GetPath("styleItem", "LogStyleItemFile");
        }

        // Constructor for unit testing
        public StyleItemService(ApplicationDbContext context, IRepository<StyleItem> repository)
        {
            _context = context;
            _repository = repository;
            _path = _repository.GetPath("styleItem", "LogStyleItemFile");
        }

        // Retrieves all StyleItems asynchronously
        public async Task<IEnumerable<StyleItem>> GetStyleItemsAsync()
        {
            return await _repository.GetAll();
        }

        // Retrieves StyleItems by a search text asynchronously
        public async Task<IEnumerable<StyleItem>> GetStyleItemsByTextAsync(string text)
        {
            try
            {
                var data = await _context.StyleItem
                    .Where(x => x.StyleItemName.ToLower().Contains(text.ToLower()) ||
                                x.StyleItemId.ToLower().Contains(text.ToLower()) ||
                                x.StyleItemDescription.ToLower().Contains(text.ToLower()))
                    .ToArrayAsync();

                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error in GetStyleItemsByTextAsync", ex);
                throw;
            }
        }

        // Retrieves a specific StyleItem by ID asynchronously
        public async Task<StyleItem> GetStyleItemByIdAsync(string id)
        {
            try
            {
                var data = await _context.StyleItem.FirstOrDefaultAsync(x => x.StyleItemId == id);
                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error in GetStyleItemByIdAsync", ex);
                throw;
            }
        }

        // Adds a new StyleItem asynchronously
        public async Task<bool> Add(string styleItemName, string styleItemDescription)
        {
            var styleItem = new StyleItem()
            {
                StyleItemName = styleItemName,
                Status = true,
                StyleItemDescription = styleItemDescription,
                StyleItemId = $"SI{DateTime.Now}",
            };
            return await _repository.Add(styleItem);
        }

        // Removes a StyleItem by ID asynchronously
        public async Task<bool> Remove(string styleItemId)
        {
            var data = await _context.StyleItem.FirstOrDefaultAsync(x => x.StyleItemId == styleItemId);
            if (data == null) return false;

            return await _repository.Delete(data);
        }

        // Updates a StyleItem's description and name by ID asynchronously
        public async Task<bool> Update(string styleItemId, string styleItemDescription, string styleItemName)
        {
            var data = await _context.StyleItem.FirstOrDefaultAsync(x => x.StyleItemId == styleItemId);
            if (data == null) return false;

            data.StyleItemDescription = styleItemDescription;
            data.StyleItemName = styleItemName;

            return await _repository.Update(data);
        }

        // Toggles the status of a StyleItem by ID asynchronously
        public async Task<bool> Update(string styleItemId)
        {
            var data = await _context.StyleItem.FirstOrDefaultAsync(x => x.StyleItemId == styleItemId);
            if (data == null) return false;

            try
            {
                data.Status = !data.Status;
                return await _repository.Update(data);
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error in Update (toggle status)", ex);
                throw;
            }
        }
        
        // Logs error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder();
            errorDetails.AppendLine($"{message}");
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
