using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.employee;
using MiniStore.Models;
using MiniStore.Services.employee;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeService;

        // Constructor to inject dependencies
        public EmployeeController(IEmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }

        // Get all employees
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var data = await _employeeService.GetEmployeesAsync();
            if (data == null)
                return NotFound(ServiceResult<string>.FailedResult("Not found"));

            // Transform data into response model
            var result = data.Select(x => new EmployeeModelRequest()
            {
                EmployeeName = x.EmployeeName,
                CCCD = x.CCCD,
                EmloyeeEmail = x.EmloyeeEmail,
                EmployeeId = x.EmployeeId,
                Phone = x.Phone,
                Status = x.Status,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<EmployeeModelRequest>>.SuccessResult(result));
        }

        // Get employees by text (search)
        [HttpGet("search")]
        public async Task<IActionResult> GetEmployeesByText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return BadRequest(ServiceResult<string>.FailedResult("Text was null"));

            var data = await _employeeService.GetEmployeesByText(text);
            if (data == null)
                return NotFound(ServiceResult<string>.FailedResult("Not found"));

            // Transform data into response model
            var result = data.Select(x => new EmployeeModelRequest()
            {
                EmployeeName = x.EmployeeName,
                CCCD = x.CCCD,
                EmloyeeEmail = x.EmloyeeEmail,
                EmployeeId = x.EmployeeId,
                Phone = x.Phone,
                Status = x.Status,
            }).ToList();

            return Ok(ServiceResult<IEnumerable<EmployeeModelRequest>>.SuccessResult(result));
        }

        // Get employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeesById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(ServiceResult<string>.FailedResult("Id was null"));

            var data = await _employeeService.GetEmployeesById(id);
            if (data == null)
                return NotFound(ServiceResult<string>.FailedResult("Not found"));

            // Transform data into response model
            var result = new EmployeeModelRequest()
            {
                EmployeeName = data.EmployeeName,
                CCCD = data.CCCD,
                EmloyeeEmail = data.EmloyeeEmail,
                EmployeeId = data.EmployeeId,
                Phone = data.Phone,
                Status = data.Status,
            };

            return Ok(ServiceResult<EmployeeModelRequest>.SuccessResult(result));
        }

        // Add a new employee
        [HttpPost]
        public async Task<IActionResult> Add(string firstName, string middleName, string lastName, string email, string phone, int cccd)
        {
            if (string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(middleName) ||
                string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) ||
                cccd == 0)
                return BadRequest(ServiceResult<string>.FailedResult("Data input was invalid"));

            var result = await _employeeService.Add(firstName, middleName, lastName, email, phone, cccd);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Add data success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't add data"));
        }

        // Update an existing employee
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Update(string employeeId, string firstName, string middleName, string lastName, string email, string phone, int cccd)
        {
            if (string.IsNullOrEmpty(employeeId) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(middleName) ||
                string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone) ||
                cccd == 0)
                return BadRequest(ServiceResult<string>.FailedResult("Data input was invalid"));

            var result = await _employeeService.Update(employeeId, firstName, middleName, lastName, email, phone, cccd);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Update data success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't update data"));
        }

        // Update employee status
        [HttpPatch("{employeeId}/status")]
        public async Task<IActionResult> Update(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest(ServiceResult<string>.FailedResult("Data input was invalid"));

            var result = await _employeeService.Update(employeeId);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Update status success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't update status"));
        }

        // Delete an employee
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> Delete(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
                return BadRequest(ServiceResult<string>.FailedResult("Data input was invalid"));

            var result = await _employeeService.Delete(employeeId);
            if (result)
                return Ok(ServiceResult<string>.SuccessResult("Delete success"));

            return BadRequest(ServiceResult<string>.FailedResult("Can't delete employee"));
        }
    }
}
