using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Shift
    {
        [Key]
        //public int Id { get; set; }
        [Column(name:"Shift Id")]
        public string ShiftId {  get; set; } = string.Empty;
        [Column(name:"Shift Name ")]
        public string ShiftName { get; set; } = string.Empty;
        public List<Calender> Calenders { get; set; } = new List<Calender>();
    }
    
}
