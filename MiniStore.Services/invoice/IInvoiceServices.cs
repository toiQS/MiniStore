using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.invoice
{
    public interface IInvoiceServices
    {
        public Task<IEnumerable<Invoice>> GetInvoicesAsync();
        public Task<Invoice> GetInvoicesByIdAsync(string invoiceid);
        public Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(string customerId);
        public Task<IEnumerable<Invoice>> GetInvoicesByEmployeeIdAsync(string employeeId);
        public Task<bool> Add(string employee, string customerId);
        public Task<bool> Update(string invoiceId, string employee, string customerId);
        public Task<bool> Update(string invoiceId);
        public Task<bool> Delete(string invoiceId);
    }
}
