using Microsoft.AspNetCore.Identity;
using MiniStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniStore.Data
{
    public class User : IdentityUser
    {
        //[ForeignKey(nameof(Employee))]
        [Column(name:"Employee Id")]
        public string EmployeeId { get; set; } = string.Empty;
        //public virtual Employee Employee { get; set; }
    }
}
