using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;
using MiniStore.Data;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.coupon
{
    public class CouponServices : ICouponService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<CouponServices> _logger;
        public CouponServices(ApplicationDbContext db, ILogger<CouponServices> logger)
        {
            try
            {
                _db = db;
                _logger = logger;
            }
            catch (Exception ex)
            {
                File.AppendAllTextAsync("/coupon/ConponLogger.txt", "Can't connect to database or can't use tool Logger\n");
                File.AppendAllTextAsync("/coupon/ConponLogger.txt", $"{ex.StackTrace} \n");
                File.AppendAllTextAsync("/coupon/ConponLogger.txt", $"{ex.Source} \n");
            }

        }
        public async Task<IEnumerable<Coupon>> GetCouponsAsync()
        {
            try
            {
                var coupons = await _db.Coupons.ToListAsync();
                return coupons;
            }
            catch (Exception ex)
            {
                var logFilePath = Path.Combine("coupon", "CouponLogger.txt");
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database");
                errorDetails.AppendLine($"Message: {ex.Message}");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}");
                errorDetails.AppendLine($"Source: {ex.Source}");
                errorDetails.AppendLine($"Time: {DateTime.Now}");

                await File.AppendAllTextAsync(logFilePath, errorDetails.ToString());
                return new List<Coupon>();
            }
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
