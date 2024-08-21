using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.Files;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
        public string GetPath()
        {
            App app = new App();
            var path = app.GetPathCurrentFolder("coupon", "LogCouponFile.txt");

            return path;
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
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return new List<Coupon>();
            }
        }
        

        public async Task<Coupon> GetCouponDetailById(string id)
        {
            try
            {
                var coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponId == id);
                return coupon;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<Coupon>> GetCouponsByText(string text)
        {
            try
            {
                var coupon = await _db.Coupons
                    .Where(
                    x => x.CouponName.ToLower().Contains(text.ToLower())
                        || x.CouponDescription.ToLower().Contains(text.ToLower())
                    )
                    .ToListAsync();
                return coupon;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                throw;
            }
        }
        public async Task<IEnumerable<Coupon>> GetCouponsByDateRange(DateTime startAt, DateTime endAt)
        {
            try
            {
                var coupon = await _db.Coupons
                .Where(
                    x => x.StartAt <= startAt && x.EndAt <= endAt
                    )
                    .ToListAsync();
                return coupon;
            }
            catch(Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return new List<Coupon>();
            }
        }
        public async Task<bool> AddCoupon(Coupon coupon)
        {
            try
            {
                await _db.Coupons.AddAsync(coupon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return false;
            }
        }
        public async Task<bool> EditCoupon(string couponId, Coupon newcoupon)
        {
            try
            {
                var coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponId == couponId);
                if (coupon == null)
                {
                    var path = GetPath();
                    await File.AppendAllTextAsync(path,"Can't find data");
                    return false;
                }
                coupon.CouponName = newcoupon.CouponName;
                coupon.CouponDescription = newcoupon.CouponDescription;
                coupon.StartAt = newcoupon.StartAt;
                coupon .EndAt = newcoupon.EndAt;
                coupon.Unit = newcoupon.Unit;
                coupon.Value = newcoupon.Value;
                coupon.ApplyToItem = newcoupon.ApplyToItem;
                _db.Coupons.Update(coupon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return false;
            }
        }
        public async Task<bool> DeleteCoupon(string couponId)
        {
            try
            {
                var coupon = await _db.Coupons.FirstOrDefaultAsync(x => x.CouponId == couponId);
                if (coupon == null)
                {
                    var path = GetPath();
                    await File.AppendAllTextAsync(path, "Can't find data");
                    return false;
                }
                _db.Coupons.Remove(coupon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var path = GetPath();
                var errorDetails = new StringBuilder();
                errorDetails.AppendLine("Can't connect to database\n");
                errorDetails.AppendLine($"Message: {ex.Message}\n");
                errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
                errorDetails.AppendLine($"Source: {ex.Source} \n");
                errorDetails.AppendLine($"Time: {DateTime.Now}\n");
                await File.AppendAllTextAsync(path, errorDetails.ToString());
                return false;
            }
        }

    }
}
