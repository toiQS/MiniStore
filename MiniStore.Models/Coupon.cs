using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Models
{
    public class Coupon
    {
        [Key]
        [Column(name:"Coupon Id")]
        public string CouponId { get; set; } = string.Empty;
        [Column(name: "Coupon Name")]
        public string CouponName {  get; set; } = string.Empty;
        [Column(name: "Coupon Description")]
        public string CouponDescription { get; set;} = string.Empty;
        [Column(name:"Type Items")]
        List<string> TypeItems { get; set; } = new List<string>();
        public float Value { get; set; }
        public string Unit { get; set; } = string.Empty ;
        [Column(name:("Start At"))]
        public DateTime StartAt { get; set; }
        [Column(name:"End At")]
        public DateTime EndAt { get; set; }

    }
}
