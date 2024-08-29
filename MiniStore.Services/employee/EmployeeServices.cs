using Microsoft.EntityFrameworkCore;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace MiniStore.Services.employee
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Employee> _employeeRepository;
        private string _path;
        //constructor for api controller
        public EmployeeServices(ApplicationDbContext context)
        {
            _context = context;
            _employeeRepository = new Repository<Employee>(context);
            _path = _employeeRepository.GetPath("employee", "LogEmployeeFile.txt");
        }
        //constructor of Unit test
        public EmployeeServices(ApplicationDbContext context, IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _context = context;
            _path = _employeeRepository.GetPath("employee", "LogEmployeeFile.txt");
        }
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _employeeRepository.GetAll();
        }
        public async Task<IEnumerable<Employee>> GetEmployeesByText(string text)
        {
            try
            {
                var data = await _context.Employee.Where(x => x.FirstName.ToLower().Contains(text.ToLower()) ||
                                                                x.MiddleName.ToLower().Contains(text.ToLower()) ||
                                                                x.LastName.ToLower().Contains(text.ToLower()) ||
                                                                x.EmloyeeEmail.ToLower().Contains(text.ToLower()) ||
                                                                x.Phone.ToLower().Contains(text.ToLower()))
                                                .AsNoTracking()
                                                .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }
        public async Task<Employee> GetEmployeesById(string id)
        {
            try
            {
                var data = await _context.Employee.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == id);
                return data;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(string.Empty, ex);
                throw;
            }
        }
        public async Task<bool> Add(string firstName, string middleName, string lastName, string email, string phone, int cccd)
        {
            var data = new Employee()
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                EmployeeName = firstName + middleName ?? string.Empty +lastName,
                Phone = phone,
                CCCD = cccd,
                EmloyeeEmail = email,
                EmployeeId = $"E{DateTime.Now}"
            };
            return await _employeeRepository.Add(data);
        }
        public async Task<bool> Update(string employeeId, string firstName, string middleName, string lastName, string email, string phone, int cccd)
        {
            var data = await _context.Employee.AsTracking().FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (data == null) return false;
            data.CCCD = cccd;
            data.MiddleName = middleName;
            data.LastName = lastName;
            data.FirstName = firstName;
            data.EmloyeeEmail = email;
            data.Phone = phone;
            data.EmployeeName = firstName + middleName ?? string.Empty + lastName;
            return await _employeeRepository.Update(data);
        }
        public async Task<bool> Update(string employeeId)
        {
            var data = await _context.Employee.AsTracking().FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (data == null) return false;
            data.Status = !data.Status;
            return await _employeeRepository.Update(data);
        }
        public async Task<bool> Delete(string employeeId)
        {
            var data = await _context.Employee.AsTracking().FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (data == null) return false;
            return await _employeeRepository.Delete(data);
        }
        // Log error details to a file asynchronously
        private async Task LogErrorAsync(string message, Exception ex)
        {
            var errorDetails = new StringBuilder(); // Initialize a StringBuilder for error details
            errorDetails.AppendLine($"{message}\n"); // Append the error message
            errorDetails.AppendLine($"Message: {ex.Message}\n"); // Append the exception message
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n"); // Append the stack trace
            errorDetails.AppendLine($"Source: {ex.Source}\n"); // Append the source of the exception
            errorDetails.AppendLine($"Time: {DateTime.Now}\n"); // Append the current time

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString()); // Write the error details to the log file
            }
        }
    }
}
