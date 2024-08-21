using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.Files;

namespace MiniStore.Services.coupon
{
    public class CouponServices : ICouponService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<CouponServices> _logger;
        public CouponServices(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            try
            {
                var coupons = await _db.Coupons
                    .AsNoTracking()
                    .ToListAsync();
                return coupons;
            }
            catch (Exception ex)
            {
                return new List<Coupon>();
            }
        }
        public string GetPath()
        {
            App app = new App();
            var path = app.GetPathCurrentFolder("coupon","LogCouponFile.txt");
            
            return path;
        }

        //public async Task<Coupon> GetCouponDetailById(string id)
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
                
        //    }
        //}
        //public Task<IEnumerable<Coupon>> GetCouponsByText(string text)
        //{

        //}
        //public Task<IEnumerable<Coupon>> GetCouponsByDateRange(DateTime startAt, DateTime endAt);
        //public Task<bool> AddCoupon(Coupon coupon);
        //public Task<bool> EditCoupon(string couponId, Coupon coupon);
        //public Task<bool> DeleteCoupon(string couponId);

    }
}
