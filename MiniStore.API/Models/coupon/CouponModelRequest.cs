using System.ComponentModel.DataAnnotations;

namespace MiniStore.API.Models.coupon
{
    public class CouponModelRequest
    {
        public string CouponId { get; set; } = string.Empty;
        public string CouponName { get; set; } = string.Empty;
        public string CouponDescription { get; set; } = string.Empty;
        public float Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string ApplyToItem { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
    }
}
