using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.receipt;
using MiniStore.Services.repository;
using MiniStore.Services.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.receipt
{
    // Service class for managing receipts
    public class ReceiptServices : IReceiptServices
    {
        private readonly ApplicationDbContext _context; // Database context
        private readonly IRepository<Receipt> _repository; // Generic repository for Receipt entities
        private static string _path = string.Empty; // Path for logging errors

        // Constructor initializing the service with a database context
        public ReceiptServices(ApplicationDbContext context)
        {
            _context = context;
            _repository = new Repository<Receipt>(context); // Initialize the repository
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt"); // Set the log file path
        }

        // Constructor initializing the service with a database context and a repository
        public ReceiptServices(ApplicationDbContext context, IRepository<Receipt> repository)
        {
            _context = context;
            _repository = repository; // Use the provided repository
            _path = _repository.GetPath("receipt", "LogReceiptFile.txt"); // Set the log file path
        }

        // Retrieve all receipts asynchronously
        public async Task<IEnumerable<Receipt>> GetReceiptsAsync()
        {
            return await _repository.GetAll(); // Get all receipts using the repository
        }

        // Retrieve a specific receipt by its ID asynchronously
        public async Task<Receipt> GetReceiptAsync(string id)
        {
            try
            {
                var receipt = await _context.Receipt
                    .AsNoTracking() // Retrieve without tracking changes
                    .FirstOrDefaultAsync(x => x.ReceiptId == id) // Find the receipt by ID
                    .ConfigureAwait(false);

                return receipt; // Return the found receipt
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to retrieve receipt", ex); // Log any errors
                throw; // Rethrow the exception
            }
        }

        // Retrieve receipts that match a text pattern asynchronously
        public async Task<IEnumerable<Receipt>> GetReceiptByText(string text)
        {
            try
            {
                var receipts = await _context.Receipt
                    .AsNoTracking() // Retrieve without tracking changes
                    .Where(x => x.ReceiptId.ToLower().Contains(text.ToLower()) ||
                                x.SupplierID.ToLower().Contains(text.ToLower())) // Match by receipt ID or supplier ID
                    .ToListAsync()
                    .ConfigureAwait(false);

                return receipts; // Return the list of matching receipts
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Failed to search receipts", ex); // Log any errors
                throw; // Rethrow the exception
            }
        }

        // Create a new receipt asynchronously
        public async Task<bool> Create(Receipt receipt)
        {
            return await _repository.Add(receipt); // Add the receipt using the repository
        }

        // Remove a receipt by its ID asynchronously
        public async Task<bool> Remove(string receiptId)
        {
            var receipt = await _context.Receipt
                .AsTracking() // Track changes to the receipt
                .FirstOrDefaultAsync(x => x.ReceiptId == receiptId) // Find the receipt by ID
                .ConfigureAwait(false);

            if (receipt == null) return false; // Return false if the receipt is not found

            return await _repository.Delete(receipt); // Delete the receipt using the repository
        }

        // Update an existing receipt asynchronously
        public async Task<bool> Update(string receiptId, Receipt newReceipt)
        {
            var receipt = await _context.Receipt
                .AsTracking() // Track changes to the receipt
                .FirstOrDefaultAsync(x => x.ReceiptId == receiptId) // Find the receipt by ID
                .ConfigureAwait(false);

            if (receipt == null) return false; // Return false if the receipt is not found

            // Update receipt properties
            receipt.Supplier = newReceipt.Supplier;
            receipt.CreateAt = newReceipt.CreateAt;

            return await _repository.Update(receipt); // Update the receipt using the repository
        }

        // Log error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder(); // Initialize a StringBuilder for error details
            errorDetails.AppendLine($"{message}\n"); // Append the error message
            errorDetails.AppendLine($"Message: {ex.Message}\n"); // Append the exception message
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n"); // Append the stack trace
            errorDetails.AppendLine($"Source: {ex.Source}\n"); // Append the source of the exception
            errorDetails.AppendLine($"Time: {DateTime.Now}\n"); // Append the current time

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString()); // Write the error details to the log file
            }
        }
    }
}
