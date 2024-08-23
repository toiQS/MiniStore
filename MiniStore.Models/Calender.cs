using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Calender
    {
        [Key]
        //public int Id { get; set; }
        [Column(name:"Calender Id")]
        public string CalenderId { get; set; } = string.Empty;
        [Column(name: "Day of week")]
        public DayOfWeek DayOfWeek { get; set; }
        [Column(name: "Stat At")]
        public TimeOnly StartAt { get; set; }
        [Column(name: "End At")]
        public TimeOnly EndAt { get; set; }

        [Column(name:"Shift Id")]
        [ForeignKey(nameof(Shift))]
        public string ShiftId { get; set; } = string.Empty;
        public virtual Shift Shift { get; set; }
    }
}
