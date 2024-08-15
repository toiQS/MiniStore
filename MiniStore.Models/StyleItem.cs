using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    [Table(name:"Style Item")]
    public class StyleItem
    {
        [Key]
        //public int Id { get; set; }
        [Column(name: "Style Item Id")]
        public string StyleItemId { get; set; } = string.Empty;
        [Column(name: "Style Item Name")]
        public string StyleItemName { get; set; }= string.Empty;
        [Column(name: "Style Item Description")]
        public string StyleItemDescription { get; set;} = string.Empty;
        public bool Status {  get; set; } 
    }
}
