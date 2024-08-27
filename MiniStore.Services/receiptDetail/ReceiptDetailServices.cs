using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.receiptDetail;
using MiniStore.Services.repository;
using MiniStore.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.receiptDetail
{
    // Service class for managing receipt details
    public class ReceiptDetailServices : IReceiptDetailServices
    {
        private readonly ApplicationDbContext _context; // Database context
        private IRepository<ReceiptDetail> _receiptDetailRepository; // Repository for ReceiptDetail entities
        private IRepository<Item> _itemRepository; // Repository for Item entities
        private readonly string _path; // Path for logging errors

        // Constructor for API controller, initializes repositories and sets the log file path
        public ReceiptDetailServices(ApplicationDbContext context)
        {
            _context = context;
            _receiptDetailRepository = new Repository<ReceiptDetail>(context); // Initialize ReceiptDetail repository
            _itemRepository = new Repository<Item>(context); // Initialize Item repository
            _path = _receiptDetailRepository.GetPath("receipt detail", "LogReceiptDetailFile.txt"); // Set log file path
        }

        // Constructor for Unit Test, allows for dependency injection of repositories
        public ReceiptDetailServices(ApplicationDbContext context, IRepository<ReceiptDetail> receiptDetailRepository, IRepository<Item> itemRepository)
        {
            _context = context;
            _receiptDetailRepository = receiptDetailRepository; // Use provided ReceiptDetail repository
            _itemRepository = itemRepository; // Use provided Item repository
            _path = _receiptDetailRepository.GetPath("receipt detail", "LogReceiptDetailFile.txt"); // Set log file path
        }

        // Fetch all receipt details from the database asynchronously
        public async Task<IEnumerable<ReceiptDetail>> GetReceiptDetailsAsync()
        {
            return await _receiptDetailRepository.GetAll(); // Get all receipt details using the repository
        }

        // Fetch a specific receipt detail by its ID asynchronously
        public async Task<ReceiptDetail> GetReceiptDetailAsync(string id)
        {
            try
            {
                var result = await _context.ReceiptDetail
                    .AsNoTracking() // Retrieve without tracking changes
                    .FirstOrDefaultAsync(x => x.ReceiptDetailId == id); // Find the receipt detail by ID
                return result; // Return the found receipt detail
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex); // Log any errors
                throw; // Rethrow the exception
            }
        }

        // Add a new receipt detail and update the item's quantity asynchronously
        public async Task<bool> AddReceipt(ReceiptDetail receiptDetail)
        {
            // Check if the item exists and is active
            var isItemExist = await _context.Item.AnyAsync(x => x.ItemId == receiptDetail.ItemId && x.Status == true);
            if (isItemExist)
            {
                // Add the receipt detail to the repository
                var result = await _receiptDetailRepository.Add(receiptDetail);

                // Update the item's quantity
                var dataItem = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == receiptDetail.ItemId);
                if (dataItem == null)
                {
                    return false; // Return false if the item is not found
                }
                dataItem.Quantity += receiptDetail.Quantity; // Increase the quantity by the receipt detail amount
                await _itemRepository.Update(dataItem); // Update the item in the repository
                return result; // Return the result of adding the receipt detail
            }
            return false; // Return false if the item does not exist
        }

        // Remove a receipt detail and update the item's quantity asynchronously
        public async Task<bool> RemoveReceipt(string id)
        {
            // Find the receipt detail by ID
            var data = await _context.ReceiptDetail
                   .AsNoTracking() // Retrieve without tracking changes
                   .FirstOrDefaultAsync(x => x.ReceiptDetailId == id);

            if (data == null) return false; // Return false if the receipt detail is not found

            // Check if the item exists and is active
            var isItemExist = await _context.Item.AnyAsync(x => x.ItemId == data.ItemId && x.Status == true);
            if (isItemExist)
            {
                // Remove the receipt detail from the repository
                var result = await _receiptDetailRepository.Delete(data);

                // Update the item's quantity
                var dataItem = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == data.ItemId);
                if (dataItem == null)
                {
                    return false; // Return false if the item is not found
                }
                dataItem.Quantity -= data.Quantity; // Decrease the quantity by the receipt detail amount
                await _itemRepository.Update(dataItem); // Update the item in the repository
                return result; // Return the result of removing the receipt detail
            }
            return false; // Return false if the item does not exist
        }

        // Update the quantity of a receipt detail and the corresponding item asynchronously
        public async Task<bool> UpdateReceipt(string id, int quantity)
        {
            // Find the receipt detail by ID
            var data = await _context.ReceiptDetail
                   .AsNoTracking() // Retrieve without tracking changes
                   .FirstOrDefaultAsync(x => x.ReceiptDetailId == id);

            if (data == null) return false; // Return false if the receipt detail is not found

            // Check if the item exists and is active
            var isItemExist = await _context.Item.AnyAsync(x => x.ItemId == data.ItemId && x.Status == true);
            if (isItemExist)
            {
                // Update the receipt detail in the repository
                var result = await _receiptDetailRepository.Update(data);

                // Update the item's quantity based on the difference between old and new quantities
                var dataItem = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == data.ItemId);
                if (dataItem == null)
                {
                    return false; // Return false if the item is not found
                }
                dataItem.Quantity += (data.Quantity - quantity); // Adjust the quantity
                await _itemRepository.Update(dataItem); // Update the item in the repository
                return result; // Return the result of updating the receipt detail
            }
            return false; // Return false if the item does not exist
        }

        // Log error details to a file asynchronously
        private async Task LogErrorAsync(Exception ex)
        {
            var errorDetails = new StringBuilder(); // Initialize a StringBuilder for error details
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
