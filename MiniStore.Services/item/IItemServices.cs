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
        public Task<bool> Add(Item item);
        public Task<bool> UpdateStatus(string itemId);
        public Task<bool> Update(string itemId, string itemName, string styleItemId);

    }
}
