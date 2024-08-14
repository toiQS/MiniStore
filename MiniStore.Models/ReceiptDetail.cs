using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    [Table(name:"Receip Detail")]
    public class ReceiptDetail
    {
        [Key]
        public int Id { get; set; }
        [Column(name: "Receipt Detail Id")]
        public string ReceiptDetailId { get; set; } = string.Empty;
        [ForeignKey(nameof(Item))]
        public string ItemId { get; set; } = string.Empty;
        public virtual Item Item { get; set; }
    }
}
