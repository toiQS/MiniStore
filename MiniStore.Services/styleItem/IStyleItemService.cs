using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.styleItem
{
    public interface IStyleItemService
    {
        public Task<IEnumerable<StyleItem>> GetStyleItemsAsync();
        public Task<IEnumerable<StyleItem>> GetStyleItemsByTextAsync(string text);
        public Task<StyleItem> GetStyleItemByIdAsync(string id);
        public Task<bool> Add(string styleItemName, string styleItemDescription);
        public Task<bool> Remove(string styleItemId);
        public Task<bool> Update(string styleItemId, string styleItemDescriptiontyle, string styleItemName);
        public Task<bool> Update(string styleItemId);

    }
}
