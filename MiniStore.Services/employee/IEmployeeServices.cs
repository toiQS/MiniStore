using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.employee
{
    public interface IEmployeeServices
    {
        public Task<IEnumerable<Employee>> GetEmployeesAsync();
        public Task<IEnumerable<Employee>> GetEmployeesByText(string text);
        public Task<Employee> GetEmployeesById(string id);
        public Task<bool> Add(string firstName, string middleName, string lastName, string email, string phone);
        public Task<bool> Update(string employeeId,string firstName, string middleName, string lastName, string email, string phone);
        public Task<bool> Update(string employeeId);
    }
}
