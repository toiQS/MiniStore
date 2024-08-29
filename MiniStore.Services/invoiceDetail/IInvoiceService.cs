using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.invoiceDetail
{
    public interface IInvoiceService
    {
        public Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailAsync();
        public Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailByInvoiceIdAsync(string invoiceId);
        public Task<InvoiceDetail> GetInvoiceDetailByInvoiceDetailIdAsync(string invoiceId);
        public Task<bool> Add(string invoiceId, string itemId, int quantity);
        public Task<bool> Update(string invoiceId, int quantity);
        public Task<bool> Delete(string invoiceId);
    }
}
