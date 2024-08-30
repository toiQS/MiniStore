using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.invoice
{
    public class InvoiceServices : IInvoiceServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly string _path;

        // Constructor for API controller
        public InvoiceServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _invoiceRepository = new Repository<Invoice>(context);
            _path = _invoiceRepository.GetPath("invoice", "LogInvoiceFile.txt");
        }

        // Constructor for unit testing, allowing dependency injection of a mock repository
        public InvoiceServices(ApplicationDbContext context, IRepository<Invoice> invoiceRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _path = _invoiceRepository.GetPath("invoice", "LogInvoiceFile.txt");
        }

        // Retrieves all invoices asynchronously
        public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
        {
            return await _invoiceRepository.GetAll();
        }

        // Retrieves an invoice by its ID asynchronously
        public async Task<Invoice> GetInvoicesByIdAsync(string invoiceId)
        {
            try
            {
                return await _context.Invoice
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.InvoiceId == invoiceId);
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error retrieving invoice by ID.", ex);
                throw;
            }
        }

        // Retrieves all invoices associated with a specific customer ID asynchronously
        public async Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(string customerId)
        {
            try
            {
                return await _context.Invoice
                    .AsNoTracking()
                    .Where(x => x.CustomerId == customerId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error retrieving invoices by customer ID.", ex);
                throw;
            }
        }

        // Retrieves all invoices associated with a specific employee ID asynchronously
        public async Task<IEnumerable<Invoice>> GetInvoicesByEmployeeIdAsync(string employeeId)
        {
            try
            {
                return await _context.Invoice
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error retrieving invoices by employee ID.", ex);
                throw;
            }
        }

        // Adds a new invoice to the database asynchronously
        public async Task<bool> Add(string employeeId, string customerId)
        {
            var invoice = new Invoice
            {
                InvoiceId = $"Bill{DateTime.Now:yyyyMMddHHmmssfff}", // Unique ID with timestamp
                CustomerId = customerId,
                EmployeeId = employeeId
            };
            return await _invoiceRepository.Add(invoice);
        }

        // Updates an existing invoice in the database asynchronously
        public async Task<bool> Update(string invoiceId, string employeeId, string customerId)
        {
            var existingInvoice = await _context.Invoice
                .AsTracking()
                .FirstOrDefaultAsync(x => x.InvoiceId == invoiceId);

            if (existingInvoice == null) return false;

            existingInvoice.CustomerId = customerId;
            existingInvoice.EmployeeId = employeeId;

            return await _invoiceRepository.Update(existingInvoice);
        }
        public async Task<bool> Update(string invoiceId)
        {
            var existingInvoice = await _context.Invoice
                .AsTracking()
                .FirstOrDefaultAsync(x => x.InvoiceId == invoiceId);

            if (existingInvoice == null) return false;

            existingInvoice.Status  = !existingInvoice.Status;

            return await _invoiceRepository.Update(existingInvoice);
        }

        // Deletes an invoice from the database asynchronously
        public async Task<bool> Delete(string invoiceId)
        {
            var existingInvoice = await _context.Invoice
                .AsTracking()
                .FirstOrDefaultAsync(x => x.InvoiceId == invoiceId);

            if (existingInvoice == null) return false;

            return await _invoiceRepository.Delete(existingInvoice);
        }

        // Logs error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder()
                .AppendLine($"{message}")
                .AppendLine($"Message: {ex.Message}")
                .AppendLine($"Stack Trace: {ex.StackTrace}")
                .AppendLine($"Source: {ex.Source}")
                .AppendLine($"Time: {DateTime.Now}");

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString());
            }
        }
    }
}
