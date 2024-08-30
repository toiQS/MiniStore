using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.Models;
using MiniStore.Services.invoice;
using System.Threading.Tasks;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceServices _invoiceServices;

        // Constructor to inject the IInvoiceServices dependency
        public InvoiceController(IInvoiceServices invoiceServices)
        {
            _invoiceServices = invoiceServices;
        }

        // Retrieves all invoices asynchronously
        [HttpGet]
        [Route("GetInvoices")]
        public async Task<IActionResult> GetInvoicesAsync()
        {
            var data = await _invoiceServices.GetInvoicesAsync();
            if (data == null)
                return Ok(ServiceResult<IEnumerable<string>>.FailedResult("Data was null"));

            return Ok(ServiceResult<IEnumerable<Invoice>>.SuccessResult(data));
        }

        // Retrieves an invoice by its ID asynchronously
        [HttpGet]
        [Route("GetInvoiceById/{invoiceId}")]
        public async Task<IActionResult> GetInvoicesByIdAsync(string invoiceId)
        {
            if (string.IsNullOrEmpty(invoiceId))
                return BadRequest(ServiceResult<string>.FailedResult("Id was invalid or null"));

            var data = await _invoiceServices.GetInvoicesByIdAsync(invoiceId);
            if (data == null)
                return BadRequest(ServiceResult<string>.FailedResult("Can't find data"));

            return Ok(ServiceResult<Invoice>.SuccessResult(data));
        }

        // Retrieves all invoices by a specific customer ID asynchronously
        [HttpGet]
        [Route("GetInvoicesByCustomerId/{customerId}")]
        public async Task<IActionResult> GetInvoicesByCustomerIdAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest(ServiceResult<string>.FailedResult("Customer ID was invalid or null"));

            var data = await _invoiceServices.GetInvoicesByCustomerIdAsync(customerId);
            if (data == null)
                return BadRequest(ServiceResult<string>.FailedResult("Can't find data"));

            return Ok(ServiceResult<IEnumerable<Invoice>>.SuccessResult(data));
        }

        // Retrieves all invoices by a specific employee ID asynchronously
        [HttpGet]
        [Route("GetInvoicesByEmployeeId/{employeeId}")]
        public async Task<IActionResult> GetInvoicesByEmployeeIdAsync(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest(ServiceResult<string>.FailedResult("Employee ID was invalid or null"));

            var data = await _invoiceServices.GetInvoicesByEmployeeIdAsync(employeeId);
            if (data == null)
                return BadRequest(ServiceResult<string>.FailedResult("Can't find data"));

            return Ok(ServiceResult<IEnumerable<Invoice>>.SuccessResult(data));
        }

        // Adds a new invoice asynchronously
        [HttpPost]
        [Route("AddInvoice")]
        public async Task<IActionResult> Add(string employee, string customerId)
        {
            if (string.IsNullOrEmpty(employee) || string.IsNullOrEmpty(customerId))
                return BadRequest(ServiceResult<string>.FailedResult("Input data was null"));

            var result = await _invoiceServices.Add(employee, customerId);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Invoice data added successfully"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't add new invoice"));
        }

        // Updates an existing invoice asynchronously
        [HttpPut]
        [Route("UpdateInvoice")]
        public async Task<IActionResult> Update(string invoiceId, string employee, string customerId)
        {
            if (string.IsNullOrEmpty(invoiceId) || string.IsNullOrEmpty(employee) || string.IsNullOrEmpty(customerId))
                return BadRequest(ServiceResult<string>.FailedResult("Input data was null"));

            var result = await _invoiceServices.Update(invoiceId, employee, customerId);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Invoice data updated successfully"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't update invoice"));
        }

        // Deletes an invoice asynchronously
        [HttpDelete]
        [Route("DeleteInvoice/{invoiceId}")]
        public async Task<IActionResult> Delete(string invoiceId)
        {
            if (string.IsNullOrEmpty(invoiceId))
                return BadRequest(ServiceResult<string>.FailedResult("Invoice ID was invalid or null"));

            var result = await _invoiceServices.Delete(invoiceId);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Invoice data deleted successfully"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't delete invoice"));
        }
    }
}
