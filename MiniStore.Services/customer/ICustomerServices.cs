using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.customer
{
    public interface ICustomerServices
    {
        public Task<IEnumerable<Customer>> GetCustomersAsync();
        public Task<IEnumerable<Customer>> GetCustomersByTextAsync(string text);
        public Task<Customer> GetCustomerById(string id);
        public Task<bool> Add(string customerName, string phone);
        public Task<bool> Update(string customerId, string customerName, string phone);
        public Task<bool> Update(string customerId);
        public Task<bool> Delete(string customerId);
    }
}
