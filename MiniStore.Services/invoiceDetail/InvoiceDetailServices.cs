using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.item;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.invoiceDetail
{
    public class InvoiceDetailServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<InvoiceDetail> _repository;
        private readonly IItemServices _itemServices;
        private readonly string _path;

        // Constructor for API controller
        public InvoiceDetailServices(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = new Repository<InvoiceDetail>(_context);
            _itemServices = new ItemServices(_context);
            _path = _repository.GetPath("invoiceDetail", "LogInvoiceDetailFile.txt") ?? throw new ArgumentNullException("Log path cannot be null");
        }

        // Constructor for unit testing
        public InvoiceDetailServices(ApplicationDbContext context, IRepository<InvoiceDetail> repository, IItemServices itemServices)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _itemServices = itemServices ?? throw new ArgumentNullException(nameof(itemServices));
            _path = _repository.GetPath("invoiceDetail", "LogInvoiceDetailFile.txt") ?? throw new ArgumentNullException("Log path cannot be null");
        }

        // Retrieves all invoice details asynchronously
        public async Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailsAsync()
        {
            return await _repository.GetAll();
        }

        // Retrieves invoice details by Invoice ID asynchronously
        public async Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailsByInvoiceIdAsync(string invoiceId)
        {
            try
            {   
                return await _context.InvoiceDetail
                                     .AsNoTracking()
                                     .Where(x => x.InvoiceId == invoiceId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to retrieve invoice details by invoice ID.", ex);
                throw;
            }
        }

        // Retrieves a specific invoice detail by InvoiceDetail ID asynchronously
        public async Task<InvoiceDetail> GetInvoiceDetailByInvoiceDetailIdAsync(string invoiceDetailId)
        {
            try
            {
                return await _context.InvoiceDetail
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(x => x.InvoiceDetailId == invoiceDetailId);
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to retrieve invoice detail by ID.", ex);
                throw;
            }
        }

        // Adds a new invoice detail asynchronously
        public async Task<bool> AddInvoiceDetailAsync(string invoiceId, string itemId, int quantity)
        {
            var invoiceDetail = new InvoiceDetail
            {
                InvoiceDetailId = $"BD{DateTime.Now:yyyyMMddHHmmssfff}",
                InvoiceId = invoiceId,
                ItemId = itemId,
                Quantity = quantity
            };

            var resultOfItem = await _itemServices.Sell(itemId, quantity);
            var resultOfInvoice = await _repository.Add(invoiceDetail);

            return resultOfInvoice && resultOfItem;
        }

        // Updates an existing invoice detail asynchronously
        public async Task<bool> UpdateInvoiceDetailAsync(string invoiceDetailId, int newQuantity)
        {
            var invoiceDetail = await _context.InvoiceDetail.AsNoTracking().FirstOrDefaultAsync(x => x.InvoiceDetailId == invoiceDetailId);
            if (invoiceDetail == null) return false;

            int quantityDifference = newQuantity - invoiceDetail.Quantity;
            invoiceDetail.Quantity = newQuantity;

            var resultOfItem = await _itemServices.Sell(invoiceDetail.ItemId, quantityDifference);
            var resultOfInvoice = await _repository.Update(invoiceDetail);

            return resultOfInvoice && resultOfItem;
        }

        // Deletes an invoice detail asynchronously
        public async Task<bool> DeleteInvoiceDetailAsync(string invoiceDetailId)
        {
            var invoiceDetail = await _context.InvoiceDetail.AsNoTracking().FirstOrDefaultAsync(x => x.InvoiceDetailId == invoiceDetailId);
            if (invoiceDetail == null) return false;

            var resultOfItem = await _itemServices.Sell(invoiceDetail.ItemId, -invoiceDetail.Quantity);
            var resultOfInvoice = await _repository.Delete(invoiceDetail);

            return resultOfInvoice && resultOfItem;
        }

        // Logs error details to a file asynchronously  
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder()
                .AppendLine($"{message}")
                .AppendLine($"Message: {ex.Message}")
                .AppendLine($"Stack Trace: {ex.StackTrace}")
                .AppendLine($"Source: {ex.Source}")
                .AppendLine($"Time: {DateTime.Now}")
                .ToString();

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails);
            }
        }
    }
}
