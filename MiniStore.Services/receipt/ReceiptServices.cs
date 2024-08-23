using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.receipt;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.receipt
{
    // Service class for managing receipts
    public class ReceiptServices : IReceiptServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Receipt> _repository;
        private static string _path = string.Empty;
        public ReceiptServices(ApplicationDbContext context, IRepository<Receipt> repository)
        {
            _repository = repository;
            _context = context;

        }

        // Get all receipts asynchronously
        public async Task<IEnumerable<Receipt>> GetReceiptsAsync()
        {
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt");
            var receipts = await _repository.GetAll();
            return receipts;
        }

        // Get a specific receipt by its ID asynchronously
        public async Task<Receipt> GetReceiptAsync(string id)
        {
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt");
            try
            {
                var receipt = await _context.Receipt
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ReceiptId == id)
                    .ConfigureAwait(false);
                return receipt;
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to retrieve receipt", ex);
                throw;
            }
        }

        // Get receipts that match a text pattern asynchronously
        public async Task<IEnumerable<Receipt>> GetReceiptByText(string text)
        {
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt");
            try
            {
                var receipts = await _context.Receipt
                    .AsNoTracking()
                    .Where(x => x.ReceiptId.ToLower().Contains(text.ToLower()) ||
                                x.SupplierID.ToLower().Contains(text.ToLower()))
                    .ToListAsync()
                    .ConfigureAwait(false);
                return receipts;
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to search receipts", ex);
                throw;
            }
        }

        // Create a new receipt asynchronously
        public async Task<bool> Create(Receipt receipt)
        {
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt");
            return await _repository.Add(receipt);
        }

        // Remove a receipt by its ID asynchronously
        public async Task<bool> Remove(string receiptId)
        {
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt");
            var receipt = await _context.Receipt
                .AsTracking()
                .FirstOrDefaultAsync(x => x.ReceiptId == receiptId)
                .ConfigureAwait(false);

            if (receipt == null) return false;

            return await _repository.Delete(receipt);
        }

        // Update an existing receipt asynchronously
        public async Task<bool> Update(string receiptId, Receipt newReceipt)
        {
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt");
            var receipt = await _context.Receipt
                .AsTracking()
                .FirstOrDefaultAsync(x => x.ReceiptId == receiptId)
                .ConfigureAwait(false);

            if (receipt == null) return false;

            // Update receipt properties
            receipt.Supplier = newReceipt.Supplier;
            receipt.CreateAt = newReceipt.CreateAt;

            return await _repository.Update(receipt);
        }

        // Log error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder();
            errorDetails.AppendLine($"{message}\n");
            errorDetails.AppendLine($"Message: {ex.Message}\n");
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
            errorDetails.AppendLine($"Source: {ex.Source}\n");
            errorDetails.AppendLine($"Time: {DateTime.Now}\n");

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString());
            }
        }
    }
}