using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using MiniStore.Services.Repository;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


namespace MiniStore.Services.styleItem
{
    public class StyleItemService :IStyleItemService
    {
        private ApplicationDbContext _context;
        private IRepository<StyleItem> _repository;
        private string _path;
        // constructor for api controller
        public StyleItemService(ApplicationDbContext context)
        {
            _context = context;
            _repository = new Repository<StyleItem>(context);
            _path = _repository.GetPath("styleItem", "LogStyleItemFile");

        }
        // contructor for unit test
        public StyleItemService(ApplicationDbContext context, IRepository<StyleItem> repository)
        {
            _repository = repository;
            _context = context;
            _path = _repository.GetPath("styleItem", "LogStyleItemFile");
        }

        public async Task<IEnumerable<StyleItem>> GetStyleItemsAsync()
        {
            return await _repository.GetAll();
        }
        public async Task<IEnumerable<StyleItem>> GetStyleItemsByTextAsync(string text)
        {
            try
            {
                var data = await _context.StyleItem.Where(x => x.StyleItemName.ToLower().Contains(text.ToLower()) ||
                                                                x.StyleItemId.ToLower().Contains(text.ToLower()) ||
                                                                x.StyleItemDescription.ToLower().Contains(text.ToLower()))
                                                    .ToArrayAsync();
                return data;
            }
            catch(Exception ex)
            {
                await LogErrorAsync(string.Empty,ex);
                throw;
            }
        }
        public async Task<StyleItem> GetStyleItemByIdAsync(string id)
        {
            try
            {
                var data = await _context.StyleItem.FirstOrDefaultAsync(x => x.StyleItemId == id);
                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }
        public async Task<bool> Add(StyleItem styleItem)
        {
            return await _repository.Add(styleItem);
        }
        public async Task<bool> Remove(string styleItemId)
        {
            var data = await _context.StyleItem.FirstOrDefaultAsync(x => x.StyleItemId == styleItemId);
            return await _repository.Delete(data);
        }
        public async Task<bool> Update(string styleItemId, string styleItemDescriptiontyle, string styleItemName)
        {
            var data = await _context.StyleItem.FirstOrDefaultAsync(x => x.StyleItemId == styleItemId);
            if(data == null) return false;
            data.StyleItemDescription = styleItemDescriptiontyle;
            data.StyleItemName = styleItemName;
            return await _repository.Update(data);
        }
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
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }
        // Log error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder(); // Initialize a StringBuilder for error details
            errorDetails.AppendLine($"{message}\n"); // Append the error message
            errorDetails.AppendLine($"Message: {ex.Message}\n"); // Append the exception message
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n"); // Append the stack trace
            errorDetails.AppendLine($"Source: {ex.Source}\n"); // Append the source of the exception
            errorDetails.AppendLine($"Time: {DateTime.Now}\n"); // Append the current time

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString()); // Write the error details to the log file
            }
        }

    }
}
