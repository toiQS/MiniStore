using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniStore.API.Models.employee
{
    public class EmployeeModelRequest
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public int CCCD { get; set; }
        [EmailAddress]
        public string EmloyeeEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
