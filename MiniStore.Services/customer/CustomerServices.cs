using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.customer
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Customer> _repository;
        private readonly string _path;

        // Constructor for API controller usage
        public CustomerServices(ApplicationDbContext context)
        {
            _context = context;
            _repository = new Repository<Customer>(context);
            _path = _repository.GetPath("customer", "LogCustomerFile.txt");
        }

        // Constructor for unit testing, allowing repository dependency injection
        public CustomerServices(ApplicationDbContext context, IRepository<Customer> repository)
        {
            _context = context;
            _repository = repository;
            _path = _repository.GetPath("customer", "LogCustomerFile.txt");
        }

        // Retrieves all customers asynchronously
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _repository.GetAll();
        }

        // Retrieves customers based on a text search asynchronously
        public async Task<IEnumerable<Customer>> GetCustomersByTextAsync(string text)
        {
            try
            {
                return await _context.Customer
                    .Where(x => x.CustomerName.ToLower().Contains(text.ToLower()))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error in GetCustomersByTextAsync", ex);
                throw;
            }
        }

        // Retrieves a customer by ID asynchronously
        public async Task<Customer> GetCustomerById(string id)
        {
            try
            {
                return await _context.Customer.FirstOrDefaultAsync(x => x.CustomerId == id);
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error in GetCustomerByIdAsync", ex);
                throw;
            }
        }

        // Adds a new customer asynchronously
        public async Task<bool> Add(string customerName, string phone)
        {
            var customer = new Customer
            {
                CustomerId = $"C{DateTime.Now.Ticks}",
                CustomerName = customerName,
                Phone = phone,
                Status = true
            };
            return await _repository.Add(customer);
        }

        // Updates an existing customer's details asynchronously
        public async Task<bool> Update(string customerId, string customerName, string phone)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (customer == null) return false;

            customer.CustomerName = customerName;
            customer.Phone = phone;
            return await _repository.Update(customer);
        }

        // Toggles the status of a customer asynchronously
        public async Task<bool> Update(string customerId)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (customer == null) return false;

            customer.Status = !customer.Status;
            return await _repository.Update(customer);
        }

        // Deletes a customer asynchronously
        public async Task<bool> Delete(string customerId)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if (customer == null) return false;

            return await _repository.Delete(customer);
        }

        // Logs error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder()
                .AppendLine($"{message}\n")
                .AppendLine($"Message: {ex.Message}\n")
                .AppendLine($"Stack Trace: {ex.StackTrace}\n")
                .AppendLine($"Source: {ex.Source}\n")
                .AppendLine($"Time: {DateTime.Now}\n");

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString());
            }
        }
    }
}
