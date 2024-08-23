using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.receipt
{
    public interface IReceiptServices
    {
        public Task<IEnumerable<Receipt>> GetReceiptsAsync();
        public Task<Receipt> GetReceiptAsync(string id);
        public Task<IEnumerable<Receipt>> GetReceiptByText(string text);
        public Task<bool> Create(Receipt receipt);
        public Task<bool> Remove(string receiptId);
        public Task<bool> Update(string receiptId, Receipt newReceipt);
    }
}
