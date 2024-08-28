using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Item
    {
        [Key]
        //public int Id { get; set; }
        [Column(name: "Item Id")]
        public string ItemId { get; set; } = string.Empty;
        [Column(name: "Item Name")]
        public string ItemName {  get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool Status { get; set; }
        [ForeignKey(nameof(StyleItem))]
        [Column(name: "Style Item Id")]
        public string StyleItemId {  get; set; } = string.Empty;
        public StyleItem StyleItem {  get; set; }
        [ForeignKey(nameof(Supplier))]
        public string SupplierId { get; set; }= string.Empty;
        public virtual Supplier Supplier {  get; set; }
    }
}
