using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        [Column(name: "Receipt Id")]
        public string ReceiptId { set; get; } = string.Empty;
        [Column(name: "Supplier Id")]
        public string SupplierID { get; set; } = string.Empty;
        [Column(name: "Create At")]
        public DateTime CreateAt { get; set; }
    }
}
