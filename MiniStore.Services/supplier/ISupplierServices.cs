using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.supplier
{
    public interface ISupplierServices
    {
        public Task<IEnumerable<Supplier>> GetSuppliersAsync();
        public Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsTrue();
        public Task<IEnumerable<Supplier>> GetSuppliersAsyncWithStatusIsFalse();
        public Task<Supplier> GetSupplierByIdAsync(string supplierId);
        public Task<IEnumerable<Supplier>> GetSupplierByText(string text);
        public Task<bool> AddSupplier(Supplier supplier);
        public Task<bool> RemoveSupplier(string supplierId);
        public Task<bool> EditStatusAndArchive(string supplierId);
        public Task<bool> UpdateSupplier(string supplierId, Supplier supplier);

    }
}
