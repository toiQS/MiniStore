using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.supplier;
using MiniStore.Models;
using MiniStore.Services.supplier;

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

        [HttpGet]
        public async Task<IActionResult> GetAllSupplier()
        {
            var result = await _supplierServices.GetSuppliersAsync();
            if (result == null) return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));
            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }
        [HttpGet("status = true")]
        public async Task<IActionResult> GetSuppliersAsyncWithStatusIsTrue()
        {
            var result = await _supplierServices.GetSuppliersAsyncWithStatusIsTrue();
            if (result == null) return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));
            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }
        [HttpGet("status = false")]
        public async Task<IActionResult> GetSuppliersAsyncWithStatusIsFalse()
        {
            var result = await _supplierServices.GetSuppliersAsyncWithStatusIsFalse();
            if (result == null) return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));
            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }
        [HttpGet("{id:string}")]
        public async Task<IActionResult> GetSupplierByIdAsync(string supplierId)
        {
            if (supplierId == null) return Ok(ServiceResult<SupplierModelResponse>.FailedResult(string.Empty));
            var data = await _supplierServices.GetSupplierByIdAsync(supplierId);
            if (data == null) return Ok(ServiceResult<SupplierModelResponse>.FailedResult(supplierId));
            var result = new SupplierModelResponse()
            {
                Status = data.Status,
                SupplierName = data.SupplierName,
                SupplierId = data.SupplierId,
            };
            return Ok(ServiceResult<SupplierModelResponse>.SuccessResult(result));
        }
        [HttpGet("{text}")]
        public async Task<IActionResult> GetSupplierByText(string text)
        {
            if (text == null) return Ok(ServiceResult<SupplierModelResponse>.FailedResult(string.Empty));
            var result = await _supplierServices.GetSupplierByText(text);
            if (result == null) return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.FailedResult("Data was null"));
            var data = result.Select(x => new SupplierModelResponse()
            {
                SupplierName = x.SupplierName,
                Status = x.Status,
                SupplierId = x.SupplierId,
            }).ToList();
            return Ok(ServiceResult<IEnumerable<SupplierModelResponse>>.SuccessResult(data));
        }
        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierModelRequest supplier)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ServiceResult<SupplierModelRequest>.FailedResult(string.Empty));

            }
            var data = new Supplier()
            {
                SupplierId = $"S{DateTime.Now}",
                SupplierName = supplier.SupplierName,
                Status = true,
                SupplierAddress = supplier.SupplierAddress,
                SupplierEmail = supplier.SupplierEmail,
                SupplierPhone = supplier.SupplierPhone,
            };
            var result = await _supplierServices.AddSupplier(data);
            if (result == false) return Ok(ServiceResult<SupplierModelRequest>.FailedResult(string.Empty));
            return Ok(ServiceResult<SupplierModelRequest>.SuccessResult(supplier));
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveSupplier(string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId))
            {
                return Ok(ServiceResult<bool>.FailedResult(string.Empty));

            }
            var result = await _supplierServices.RemoveSupplier(supplierId);
            if (result == false) return Ok(ServiceResult<bool>.FailedResult(string.Empty));
            return Ok(ServiceResult<bool>.SuccessResult(true));
        }
        [HttpPut("Edit Status")]
        public async Task<IActionResult> EditStatusAndArchive(string supplierId, bool status)
        {
            if (string.IsNullOrEmpty(supplierId) || !ModelState.IsValid)
            {
                return Ok(ServiceResult<bool>.FailedResult(string.Empty));

            }
            var result = await _supplierServices.RemoveSupplier(supplierId);
            if (result == false) return Ok(ServiceResult<bool>.FailedResult(string.Empty));
            return Ok(ServiceResult<bool>.SuccessResult(true));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSupplier(string supplierId, SupplierModelRequest supplier)
        {
            if (string.IsNullOrEmpty(supplierId) || !ModelState.IsValid)
            {
                return Ok(ServiceResult<bool>.FailedResult(string.Empty));

            }
            var data = new Supplier()
            {
                SupplierName = supplier.SupplierName,
                SupplierAddress = supplier.SupplierAddress,
                SupplierEmail = supplier.SupplierEmail,
                SupplierPhone = supplier.SupplierPhone,
            };
            var result = await _supplierServices.UpdateSupplier(supplierId,data);
            if (result == false) return Ok(ServiceResult<bool>.FailedResult(string.Empty));
            return Ok(ServiceResult<bool>.SuccessResult(true));
        }
    }
}
