using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Column(name:"Invoice Id")]
        public string InvoiceId { get; set; } = string.Empty;

        [ForeignKey(nameof(Employee))]
        [Column(name: "Employee Id")]
        public string EmployeeId { get; set; } = string.Empty;
        public Employee Employee { get; set; }

        [ForeignKey(nameof(Customer))]
        [Column(name: "Customer Id")]
        public string CustomerId { get; set; } = string.Empty;
        public Customer Customer { get; set; }

        [ForeignKey(nameof(Coupon))]
        [Column(name:"Coupon Id")]
        public string CouponId { get;set; } = string.Empty;
        public Coupon Coupon { get; set; }
    }
}
