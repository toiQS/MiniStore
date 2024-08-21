using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.Files;
using System.Text;

namespace MiniStore.Services.supplier
{
    public class SupplierServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SupplierServices> _logger;
        public SupplierServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public string GetPath()
        {
            App app = new App();
            var path = app.GetPathCurrentFolder("supplier", "LogSupplierFile.txt");
            return path;
        }
        public async Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            try
            {
                var supplier = await _context
                    .Suppliers
                    .AsNoTracking()
                    .ToListAsync();
                return supplier;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return new List<Supplier>();
            }
        }
        public async Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsTrue()
        {
            try
            {
                var supplier = await _context
                    .Suppliers
                    .Where(x  => x.Status == true)
                    .AsNoTracking()
                    .ToListAsync();
                return supplier;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return new List<Supplier>();
            }
        }
        public async Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsFalse()
        {
            try
            {
                var supplier = await _context
                    .Suppliers
                    .Where(x  => x.Status == false)
                    .AsNoTracking()
                    .ToListAsync();
                return supplier;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return new List<Supplier>();
            }
        }
        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.SupplierId == supplierId);
                return supplier;
            }
            catch (Exception ex)
            {

                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<Supplier>> GetSupplierByText(string text)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .AsNoTracking()
                    .Where(x =>
                       x.SupplierId.ToLower().Contains(text.ToLower())
                    || x.SupplierName.ToLower().Contains(text.ToLower())
                    || x.SupplierAddress.ToLower().Contains(text.ToLower())
                    || x.SupplierPhone.ToLower().Contains(text.ToLower())
                    || x.SupplierEmail.ToLower().Contains(text.ToLower())

                    ).ToListAsync();
                return supplier;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return new List<Supplier>();
            }
        }
        public async Task<bool> AddSupplier(Supplier supplier)
        {
            try
            {
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return false;
            }
        }
        public async Task<bool> RemoveSupplier(string supplierId)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.SupplierId == supplierId);
                if (supplier == null)
                {
                    return false;
                }
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return false;
            }
        }
        public async Task<bool> EditStatusAndArchive(string supplierId)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.SupplierId == supplierId);
                if (supplier == null)
                {
                    return false;
                }
                supplier.Status = false;
                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return false;
            }
        }

        public async Task<bool> UpdateSupplier(string supplierId, Supplier newSupplier)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .AsTracking()
                    .FirstOrDefaultAsync(x => x.SupplierId == supplierId);
                if (supplier == null)
                {
                    return false;
                }
                supplier.SupplierName = newSupplier.SupplierName;
                supplier.SupplierPhone = newSupplier.SupplierPhone;
                supplier.SupplierEmail = newSupplier.SupplierEmail;
                supplier.Status = newSupplier.Status;
                supplier.SupplierAddress = newSupplier.SupplierAddress;
                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return false;
            }

        }

    }
}
