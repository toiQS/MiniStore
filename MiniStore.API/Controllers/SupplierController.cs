using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.supplier;
using MiniStore.Models;
using MiniStore.Services.supplier;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierServices _supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

        // Retrieves all suppliers asynchronously
        [HttpGet]
        public async Task<IActionResult> GetAllSupplier()
        {
            var result = await _supplierServices.GetSuppliersAsync();
            if (result == null)
                return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));

            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // Retrieves suppliers with status true asynchronously
        [HttpGet("status=true")]
        public async Task<IActionResult> GetSuppliersAsyncWithStatusIsTrue()
        {
            var result = await _supplierServices.GetSuppliersAsyncWithStatusIsTrue();
            if (result == null)
                return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));

            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // Retrieves suppliers with status false asynchronously
        [HttpGet("status=false")]
        public async Task<IActionResult> GetSuppliersAsyncWithStatusIsFalse()
        {
            var result = await _supplierServices.GetSuppliersAsyncWithStatusIsFalse();
            if (result == null)
                return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));

            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // Retrieves a specific supplier by ID asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierByIdAsync(string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId))
                return Ok(ServiceResult<SupplierModelResponse>.FailedResult("Supplier ID was null"));

            var data = await _supplierServices.GetSupplierByIdAsync(supplierId);
            if (data == null)
                return Ok(ServiceResult<SupplierModelResponse>.FailedResult("Supplier not found"));

            var result = new SupplierModelResponse()
            {
                Status = data.Status,
                SupplierName = data.SupplierName,
                SupplierId = data.SupplierId,
            };

            return Ok(ServiceResult<SupplierModelResponse>.SuccessResult(result));
        }

        // Retrieves suppliers based on a search text asynchronously
        [HttpGet("search/{text}")]
        public async Task<IActionResult> GetSupplierByText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Ok(ServiceResult<SupplierModelResponse>.FailedResult("Search text was null"));

            var result = await _supplierServices.GetSupplierByText(text);
            if (result == null)
                return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));

            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // Adds a new supplier asynchronously
        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierModelRequest supplier)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ServiceResult<SupplierModelRequest>.FailedResult("Invalid data"));
            }

            var newSupplier = new Supplier()
            {
                SupplierId = $"S{DateTime.Now}", // Example Supplier ID generation, consider adjusting
                SupplierName = supplier.SupplierName,
                Status = true,
                SupplierAddress = supplier.SupplierAddress,
                SupplierEmail = supplier.SupplierEmail,
                SupplierPhone = supplier.SupplierPhone,
            };

            var result = await _supplierServices.AddSupplier(newSupplier);
            if (!result)
                return Ok(ServiceResult<SupplierModelRequest>.FailedResult("Failed to add supplier"));

            return Ok(ServiceResult<SupplierModelRequest>.SuccessResult(supplier));
        }

        // Removes a supplier by ID asynchronously
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSupplier(string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId))
            {
                return Ok(ServiceResult<bool>.FailedResult("Supplier ID was null"));
            }

            var result = await _supplierServices.RemoveSupplier(supplierId);
            if (!result)
                return Ok(ServiceResult<bool>.FailedResult("Failed to remove supplier"));

            return Ok(ServiceResult<bool>.SuccessResult(true));
        }

        // Edits the status and archives a supplier asynchronously
        [HttpPut("EditStatus")]
        public async Task<IActionResult> EditStatusAndArchive(string supplierId, bool status)
        {
            if (string.IsNullOrEmpty(supplierId) || !ModelState.IsValid)
            {
                return Ok(ServiceResult<bool>.FailedResult("Invalid data"));
            }

            var result = await _supplierServices.EditStatusAndArchive(supplierId, status);
            if (!result)
                return Ok(ServiceResult<bool>.FailedResult("Failed to edit status"));

            return Ok(ServiceResult<bool>.SuccessResult(true));
        }

        // Updates an existing supplier by ID asynchronously
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(string supplierId, SupplierModelRequest supplier)
        {
            if (string.IsNullOrEmpty(supplierId) || !ModelState.IsValid)
            {
                return Ok(ServiceResult<bool>.FailedResult("Invalid data"));
            }

            var updatedSupplier = new Supplier()
            {
                SupplierName = supplier.SupplierName,
                SupplierAddress = supplier.SupplierAddress,
                SupplierEmail = supplier.SupplierEmail,
                SupplierPhone = supplier.SupplierPhone,
            };

            var result = await _supplierServices.UpdateSupplier(supplierId, updatedSupplier);
            if (!result)
                return Ok(ServiceResult<bool>.FailedResult("Failed to update supplier"));

            return Ok(ServiceResult<bool>.SuccessResult(true));
        }
    }
}
