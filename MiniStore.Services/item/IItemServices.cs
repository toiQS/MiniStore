using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.item
{
    public interface IItemServices
    {
        public Task<IEnumerable<Item>> GetItemsAsync();
        public Task<IEnumerable<Item>> GetItemsStatusIsTrueAsync();
        public Task<IEnumerable<Item>> GetItemsStatusIsFalseAsync();
        public Task<Item> GetItemAsync(string id);
        public Task<IEnumerable<Item>> GetItemsAsyncByText(string text);
        public Task<bool> AddNewItem(string itemName, int quantity, string styleItemId, string supplierId);
        public Task<bool> UpdateItem(string itemId, int quantity);
        public Task<bool> UpdateInfoItem(string itemId, string itemName, string styleItemId, string supplierId);
        public Task<bool> DeleteItemAsync(string itemId);
        public Task<bool> UpdateStatusItem(string itemId);
    }
}
