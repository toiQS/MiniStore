using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using MiniStore.Services.Repository;
using System.Data.Common;
using System.Text;

namespace MiniStore.Services.supplier
{
    public class SupplierServices : ISupplierServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Supplier> _repository;
        private readonly string _path;

        // Constructor for API Controller
        public SupplierServices(ApplicationDbContext context)
        {
            _context = context;
            _repository = new Repository<Supplier>(context);
            _path = _repository.GetPath("supplier", "LogSupplierFile.txt");
        }

        // Constructor for Unit Test
        public SupplierServices(ApplicationDbContext context, IRepository<Supplier> repository)
        {
            _context = context;
            _repository = repository;
            _path = _repository.GetPath("supplier", "LogSupplierFile.txt");
        }

        // Fetch all suppliers from the database
        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return await _repository.GetAll();
        }

        // Fetch all suppliers with Status = true
        public async Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsTrue()
        {
            try
            {
                return await _context.Supplier
                                     .Where(x => x.Status == true)
                                     .AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error fetching suppliers with status = true", ex);
                throw;
            }
        }

        // Fetch all suppliers with Status = false
        public async Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsFalse()
        {
            try
            {
                return await _context.Supplier
                                     .Where(x => x.Status == false)
                                     .AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync("Error fetching suppliers with status = false", ex);
                throw;
            }
        }

        // Fetch a supplier by its ID
        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            try
            {
                return await _context.Supplier.Where(x => x.SupplierId == supplierId).AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync($"Error fetching supplier with ID: {supplierId}", ex);
                throw;
            }
        }

        // Fetch suppliers by a search text (searches in name, email, phone, and address)
        public async Task<IEnumerable<Supplier>> GetSupplierByText(string text)
        {
            try
            {
                return await _context.Supplier
                                     .Where(x => x.Status == true ||
                                                 x.SupplierEmail.ToLower().Contains(text.ToLower()) ||
                                                 x.SupplierPhone.ToLower().Contains(text.ToLower()) ||
                                                 x.SupplierName.ToLower().Contains(text.ToLower()) ||
                                                 x.SupplierAddress.ToLower().Contains(text.ToLower()))
                                     .AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                await LogErrorAsync($"Error searching suppliers with text: {text}", ex);
                throw;
            }
        }

        // Add a new supplier to the database
        public async Task<bool> AddSupplier(string supplierName, string supplierPhone, string supplierEmail, string supplierAddress)
        {
            var supplier = new Supplier()
            {
                SupplierId = $"S{DateTime.Now}",
                SupplierName = supplierName ,
                Status = true ,
                SupplierAddress = supplierAddress , 
                SupplierEmail = supplierEmail ,
                SupplierPhone = supplierPhone ,
            };
            return await _repository.Add(supplier);
        }

        // Remove a supplier from the database by its ID
        public async Task<bool> RemoveSupplier(string supplierId)
        {
            var data = await _context.Supplier
                                     .Where(x => x.SupplierId == supplierId)
                                     .AsTracking()
                                     .FirstOrDefaultAsync();
            if (data == null) return false;

            return await _repository.Delete(data);
        }

        // Edit the status of a supplier and archive it
        public async Task<bool> EditStatusAndArchive(string supplierId)
        {
            var data = await _context.Supplier
                                     .Where(x => x.SupplierId == supplierId)
                                     .AsTracking()
                                     .FirstOrDefaultAsync();
            if (data == null) return false;

            data.Status = !data.Status;
            return await _repository.Update(data);
        }

        // Update supplier information
        public async Task<bool> UpdateSupplier(string supplierId, string supplierName, string supplierPhone, string supplierEmail, string supplierAddress)
        {
            var data = await _context.Supplier
                                     .Where(x => x.SupplierId == supplierId)
                                     .AsTracking()
                                     .FirstOrDefaultAsync();
            if (data == null) return false;

            data.SupplierName = supplierName;
            data.SupplierPhone = supplierPhone;
            data.SupplierEmail = supplierEmail;
            data.SupplierAddress = supplierAddress;

            return await _repository.Update(data);
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
