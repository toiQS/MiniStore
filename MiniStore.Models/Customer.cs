using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Customer
    {
        [Key]
        [Column(name:"Customer Id")]
        public string CustomerId { get; set; } = string.Empty;
        [Column(name:"Customer Name")]
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
