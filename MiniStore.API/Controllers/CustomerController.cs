using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.Models;
using MiniStore.Services.customer;
using System.Threading.Tasks;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        // Constructor to inject customer services
        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        // Retrieves all customers asynchronously
        [HttpGet("all")]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var data = await _customerServices.GetCustomersAsync();
            if (data == null || !data.Any())
            {
                return NotFound(ServiceResult<string>.FailedResult("No customers found."));
            }
            return Ok(ServiceResult<IEnumerable<Customer>>.SuccessResult(data));
        }

        // Retrieves customers based on a text search asynchronously
        [HttpGet("search")]
        public async Task<IActionResult> GetCustomersByTextAsync(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest(ServiceResult<string>.FailedResult("Search text cannot be null or empty."));
            }

            var data = await _customerServices.GetCustomersByTextAsync(text);
            if (data == null || !data.Any())
            {
                return NotFound(ServiceResult<string>.FailedResult("No customers found matching the search criteria."));
            }

            return Ok(ServiceResult<IEnumerable<Customer>>.SuccessResult(data));
        }

        // Retrieves a customer by ID asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(ServiceResult<string>.FailedResult("Customer ID cannot be null or empty."));
            }

            var data = await _customerServices.GetCustomerById(id);
            if (data == null)
            {
                return NotFound(ServiceResult<string>.FailedResult("Customer not found."));
            }

            return Ok(ServiceResult<Customer>.SuccessResult(data));
        }

        // Adds a new customer asynchronously
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(string customerName, string phone)
        {
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(phone))
            {
                return BadRequest(ServiceResult<string>.FailedResult("Customer name and phone number cannot be null or empty."));
            }

            var result = await _customerServices.Add(customerName, phone);
            if (result)
            {
                return Ok(ServiceResult<string>.SuccessResult("Customer added successfully."));
            }

            return BadRequest(ServiceResult<string>.FailedResult("Failed to add new customer."));
        }

        // Updates an existing customer's details asynchronously
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(string customerId, string customerName, string phone)
        {
            if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(customerName) || string.IsNullOrEmpty(phone))
            {
                return BadRequest(ServiceResult<string>.FailedResult("Customer ID, name, and phone number cannot be null or empty."));
            }

            var result = await _customerServices.Update(customerId, customerName, phone);
            if (result)
            {
                return Ok(ServiceResult<string>.SuccessResult("Customer updated successfully."));
            }

            return BadRequest(ServiceResult<string>.FailedResult("Failed to update customer details."));
        }

        // Toggles the status of a customer asynchronously
        [HttpPut("toggle-status")]
        public async Task<IActionResult> UpdateStatusAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest(ServiceResult<string>.FailedResult("Customer ID cannot be null or empty."));
            }

            var result = await _customerServices.Update(customerId);
            if (result)
            {
                return Ok(ServiceResult<string>.SuccessResult("Customer status updated successfully."));
            }

            return BadRequest(ServiceResult<string>.FailedResult("Failed to update customer status."));
        }

        // Deletes a customer asynchronously
        [HttpDelete("delete/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest(ServiceResult<string>.FailedResult("Customer ID cannot be null or empty."));
            }

            var result = await _customerServices.Delete(customerId);
            if (result)
            {
                return Ok(ServiceResult<string>.SuccessResult("Customer deleted successfully."));
            }

            return BadRequest(ServiceResult<string>.FailedResult("Failed to delete customer."));
        }
    }
}
