using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.coupon
{
    public interface ICouponService
    {
        public Task<IEnumerable<Coupon>> GetCouponsAsync();
        //public Task<IEnumerable<Coupon>> GetCouponDetailById(string id);
        //public Task<IEnumerable<Coupon>> GetCouponsByText(string text);
        //public Task<IEnumerable<Coupon>> GetCouponsByDateRange(DateTime startAt , DateTime endAt);
        //public Task<bool> AddCoupon (Coupon coupon);
        //public Task<bool> EditCoupon(string couponId, Coupon coupon);
        //public Task< bool> DeleteCoupon(string couponId);
    }
}
