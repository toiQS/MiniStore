using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.supplier;
using MiniStore.Models;
using MiniStore.Services.supplier;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierServices _supplierServices;

        // Constructor to inject the supplier service
        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

        // GET: api/Supplier
        // Retrieves all suppliers asynchronously
        [HttpGet]
        public async Task<IActionResult> GetAllSupplier()
        {
            var result = await _supplierServices.GetSuppliersAsync();
            if (result == null || !result.Any())
            {
                return NotFound(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("No suppliers found."));
            }

            var data = result.Select(x => new SupplierModelResponse
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // GET: api/Supplier/status=true
        // Retrieves suppliers with status set to true asynchronously
        [HttpGet("status=true")]
        public async Task<IActionResult> GetSuppliersWithStatusTrue()
        {
            var result = await _supplierServices.GetSuppliersAsyncWithStatusIsTrue();
            if (result == null || !result.Any())
            {
                return NotFound(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("No active suppliers found."));
            }

            var data = result.Select(x => new SupplierModelResponse
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // GET: api/Supplier/status=false
        // Retrieves suppliers with status set to false asynchronously
        [HttpGet("status=false")]
        public async Task<IActionResult> GetSuppliersWithStatusFalse()
        {
            var result = await _supplierServices.GetSuppliersAsyncWithStatusIsFalse();
            if (result == null || !result.Any())
            {
                return NotFound(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("No inactive suppliers found."));
            }

            var data = result.Select(x => new SupplierModelResponse
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // GET: api/Supplier/{supplierId}
        // Retrieves a specific supplier by ID asynchronously
        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplierByIdAsync(string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId))
            {
                return BadRequest(ServiceResult<SupplierModelResponse>.FailedResult("Supplier ID was null or empty."));
            }

            var data = await _supplierServices.GetSupplierByIdAsync(supplierId);
            if (data == null)
            {
                return NotFound(ServiceResult<SupplierModelResponse>.FailedResult("Supplier not found."));
            }

            var result = new SupplierModelResponse
            {
                SupplierName = data.SupplierName,
                Status = data.Status,
                SupplierId = data.SupplierId
            };

            return Ok(ServiceResult<SupplierModelResponse>.SuccessResult(result));
        }

        // GET: api/Supplier/search/{text}
        // Retrieves suppliers based on a search text asynchronously
        [HttpGet("search/{text}")]
        public async Task<IActionResult> GetSupplierByText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Search text was null or empty."));
            }

            var result = await _supplierServices.GetSupplierByText(text);
            if (result == null || !result.Any())
            {
                return NotFound(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("No suppliers found matching the search text."));
            }

            var data = result.Select(x => new SupplierModelResponse
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId
            }).ToList();

            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }

        // POST: api/Supplier
        // Adds a new supplier asynchronously
        [HttpPost]
        public async Task<IActionResult> AddSupplier(string supplierName, string supplierPhone, string supplierEmail, string supplierAddress)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(supplierName) || string.IsNullOrEmpty(supplierPhone) || string.IsNullOrEmpty(supplierEmail) || string.IsNullOrEmpty(supplierAddress))
            {
                return BadRequest(ServiceResult<SupplierModelRequest>.FailedResult("Invalid supplier data."));
            }

            var result = await _supplierServices.AddSupplier(supplierName, supplierPhone, supplierEmail, supplierAddress);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<string>.FailedResult("Failed to add supplier."));
            }

            return Ok(ServiceResult<string>.SuccessResult("Add a new supplier success"));
        }

        // DELETE: api/Supplier/{supplierId}
        // Removes a supplier by ID asynchronously
        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> RemoveSupplier(string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId))
            {
                return BadRequest(ServiceResult<bool>.FailedResult("Supplier ID was null or empty."));
            }

            var result = await _supplierServices.RemoveSupplier(supplierId);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<bool>.FailedResult("Failed to remove supplier."));
            }

            return NoContent();
        }

        // PUT: api/Supplier/EditStatus
        // Edits the status of a supplier asynchronously
        [HttpPut("EditStatus")]
        public async Task<IActionResult> EditStatusAndArchive([FromQuery] string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId))
            {
                return BadRequest(ServiceResult<bool>.FailedResult("Supplier ID was null or empty."));
            }

            var result = await _supplierServices.EditStatusAndArchive(supplierId);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<bool>.FailedResult("Failed to update supplier status."));
            }

            return Ok(ServiceResult<bool>.SuccessResult(true));
        }

        // PUT: api/Supplier/{supplierId}
        // Updates an existing supplier by ID asynchronously
        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(string supplierId, string supplierName, string supplierPhone, string supplierEmail, string supplierAddress)
        {
            if (string.IsNullOrEmpty(supplierId) || !ModelState.IsValid || string.IsNullOrEmpty(supplierName) || string.IsNullOrEmpty(supplierPhone) || string.IsNullOrEmpty(supplierEmail) || string.IsNullOrEmpty(supplierAddress))
            {
                return BadRequest(ServiceResult<bool>.FailedResult("Invalid data or supplier ID."));
            }

            var result = await _supplierServices.UpdateSupplier(supplierId, supplierName, supplierPhone, supplierEmail, supplierAddress);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<string>.FailedResult("Failed to update supplier."));
            }

            return Ok(ServiceResult<string>.SuccessResult("Update a supplier success"));
        }
    }
}
