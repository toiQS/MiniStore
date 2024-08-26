using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using MiniStore.Services.Repository;
using System.Text;

namespace MiniStore.Services.supplier
{
    public class SupplierServices : ISupplierServices
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Supplier> _repository;
        private string _path;
        public SupplierServices(ApplicationDbContext context)
        {
            _context = context;
            _repository = new Repository<Supplier>(context);
            _path = _repository.GetPath("supplier", "LogSupplierFile.txt");
        }
        public SupplierServices(ApplicationDbContext context, IRepository<Supplier> repository)
        {
            _context = context;
            _repository = repository;
            _path = _repository.GetPath("supplier", "LogSupplierFile.txt");
        }
        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            var result = await _repository.GetAll();
            return result;  
        }
        public async Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsTrue()
        {
            try
            {
                var result = await _context.Supplier
                 .Where(x => x.Status == true)
                 .AsNoTracking()
                 .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsFalse()
        {
            try
            {
                var result = await _context.Supplier
                 .Where(x => x.Status == false)
                 .AsNoTracking()
                 .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }
        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            try
            {
                var result = await _context.Supplier
                                 .Where(x => x.SupplierId == supplierId)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }
        public async Task<IEnumerable<Supplier>> GetSupplierByText(string text)
        {
            try
            {
                var result = await _context.Supplier
                 .Where(x => x.Status == true ||
                             x.SupplierEmail.ToLower().Contains(text.ToLower()) ||
                             x.SupplierPhone.ToLower().Contains(text.ToLower()) ||
                             x.SupplierName.ToLower().Contains(text.ToLower())  ||
                             x.SupplierAddress.ToLower().Contains(text.ToLower()))
                 .AsNoTracking()
                 .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }

        public async Task<bool> AddSupplier(Supplier supplier)
        {
            var result = await _repository.Add(supplier);
            return result;
        }
        public async Task<bool> RemoveSupplier(string supplierId)
        {
            var data = await _context.Supplier
                                  .Where(x => x.SupplierId == supplierId)
                                  .AsTracking()
                                  .FirstOrDefaultAsync();
            if (data == null) return false;
            var result = await _repository.Delete(data);
            return result;
        }
        public async Task<bool> EditStatusAndArchive(string supplierId, bool status)
        {
            var data = await _context.Supplier
                                  .Where(x => x.SupplierId == supplierId)
                                  .AsTracking()
                                  .FirstOrDefaultAsync();
            if (data == null) return false;
            data.Status = status;
            var result = await _repository.Update(data);
            return result;
        }
        public async Task<bool> UpdateSupplier(string supplierId, Supplier supplier)
        {
            var data = await _context.Supplier
                                  .Where(x => x.SupplierId == supplierId)
                                  .AsTracking()
                                  .FirstOrDefaultAsync();
            if (data == null) return false;
            data.SupplierName = supplier.SupplierName;
            data.SupplierPhone = supplier.SupplierPhone;
            data.SupplierEmail = supplier.SupplierEmail;
            data.SupplierAddress = supplier.SupplierAddress;
            var result = await _repository.Update(data); 
            return result;
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
