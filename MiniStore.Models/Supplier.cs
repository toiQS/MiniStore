using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Supplier
    {
        [Key]
        //public int Id { get; set; }
        [Column(name: "Supplier Id")]
        public string SupplierId { get; set; } = string.Empty;
        [Column(name:"Supplier Name")]
        public string SupplierName { get; set; } = string.Empty;
        [Column(name:"Supplier Phone")]
        public string SupplierPhone {  get; set; } = string.Empty;
        [Column(name: "Supplier Email")]
        public string SupplierEmail { get; set;} = string.Empty;
        [Column(name:"Suppler Address")]
        public string SupplierAddress {  get; set; } = string.Empty;
        [Column(name: "Supplier Status")]
        public bool Status { get; set; } 
    }
}
