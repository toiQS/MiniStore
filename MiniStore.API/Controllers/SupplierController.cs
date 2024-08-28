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

        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

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

        // Retrieves suppliers with status true asynchronously
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

        // Retrieves suppliers with status false asynchronously
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

        // Adds a new supplier asynchronously
        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierModelRequest supplier)
        {
            if (!ModelState.IsValid || supplier == null)
            {
                return BadRequest(ServiceResult<SupplierModelRequest>.FailedResult("Invalid supplier data."));
            }

            var newSupplier = new Supplier
            {
                SupplierId = $"S{DateTime.Now:yyyyMMddHHmmss}", // Unique ID with timestamp
                SupplierName = supplier.SupplierName,
                Status = true,
                SupplierAddress = supplier.SupplierAddress,
                SupplierEmail = supplier.SupplierEmail,
                SupplierPhone = supplier.SupplierPhone
            };

            var result = await _supplierServices.AddSupplier(newSupplier);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<SupplierModelRequest>.FailedResult("Failed to add supplier."));
            }

            return Ok(ServiceResult<SupplierModelRequest>.SuccessResult(supplier));
        }

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

        // Updates an existing supplier by ID asynchronously
        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(string supplierId, SupplierModelRequest supplier)
        {
            if (string.IsNullOrEmpty(supplierId) || !ModelState.IsValid || supplier == null)
            {
                return BadRequest(ServiceResult<bool>.FailedResult("Invalid data or supplier ID."));
            }

            var updatedSupplier = new Supplier
            {
                SupplierName = supplier.SupplierName,
                SupplierAddress = supplier.SupplierAddress,
                SupplierEmail = supplier.SupplierEmail,
                SupplierPhone = supplier.SupplierPhone
            };

            var result = await _supplierServices.UpdateSupplier(supplierId, updatedSupplier);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ServiceResult<bool>.FailedResult("Failed to update supplier."));
            }

            return Ok(ServiceResult<bool>.SuccessResult(true));
        }
    }
}
