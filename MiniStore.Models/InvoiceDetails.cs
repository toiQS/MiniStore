using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    [Table(name: "Invoice Detail")]
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }
        [Column(name:"Invoice Detail Id")]
        public string InvoiceDetailId { get; set; } = string.Empty;

        [ForeignKey(nameof(Item))]
        [Column(name:"Item Id")]
        public string ItemId { get; set; } = string.Empty ;
        public Item Item { get; set; }

        [ForeignKey(nameof(Invoice))]
        [Column(name: "Invoice Id")]
        public string InvoiceId { get; set; } = string.Empty;
        public Invoice Invoice { get; set; }

        public int Quantity { get; set; }
    }
}
