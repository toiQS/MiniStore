using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStore.API.Models;
using MiniStore.API.Models.employee;
using MiniStore.Models;
using MiniStore.Services.employee;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeService;
        public EmployeeController(IEmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var data = await _employeeService.GetEmployeesAsync();
            if (data == null) return NotFound(ServiceResult<string>.FailedResult("not found"));
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
        public async Task<IActionResult> GetEmployeesByText(string text)
        {
            if (string.IsNullOrEmpty(text)) return BadRequest(ServiceResult<string>.FailedResult("text was null"));
            var data = await _employeeService.GetEmployeesByText(text);
            if (data == null) return NotFound(BadRequest(ServiceResult<string>.FailedResult("not found ")));
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
        public async Task<IActionResult> GetEmployeesById(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest(ServiceResult<string>.FailedResult("id was null"));
            var data = await _employeeService.GetEmployeesById(id);
            if (data == null) return NotFound(ServiceResult<IActionResult>.FailedResult("Not found"));
            var result = new EmployeeModelRequest()
            {
                EmployeeName= data.EmployeeName,
                CCCD= data.CCCD,
                EmloyeeEmail= data.EmloyeeEmail,
                EmployeeId= data.EmployeeId,    
                Phone= data.Phone,  
                Status= data.Status,
            };
            return Ok(ServiceResult<EmployeeModelRequest>.SuccessResult(result));
        }
        public Task<IActionResult> Add(string firstName, string middleName, string lastName, string email, string phone, int cccd)
        {
            
        }
        public Task<IActionResult> Update(string employeeId, string firstName, string middleName, string lastName, string email, string phone, int cccd);
        public Task<IActionResult> Update(string employeeId);
        public Task<IActionResult> Delete(string employeeId);
    }
}
