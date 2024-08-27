using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.receiptDetail
{
    public interface IReceiptDetailServices
    {
        public Task<IEnumerable<ReceiptDetail>> GetReceiptDetailsAsync();
        public Task<ReceiptDetail> GetReceiptDetailAsync(string id);
        public Task<bool> AddReceipt(ReceiptDetail receiptDetail);
        public Task<bool> RemoveReceipt(string id);
        public Task<bool> UpdateReceipt(string id, int quantity);
    }
}
