using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Employee
    {
        [Key]
        //public int Id { get; set; }
        [Column(name: "Employee Id")]
        public string EmployeeId { get; set; } = string.Empty;
        [Column(name:"First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Column(name: "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Column(name: "Middle Name")]
        public string? MiddleName { get; set; } = string.Empty;
        [Column(name: "Employee Name")]
        public string EmployeeName { get; set; } = string.Empty;
        public int CCCD { get; set; }
        [Column(name:"Employee Email")]
        public string EmloyeeEmail { get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
        public bool Status {  get; set; }
    }
}
