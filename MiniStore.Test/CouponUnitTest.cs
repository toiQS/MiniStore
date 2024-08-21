using MiniStore.Data;
using MiniStore.Services.coupon;
using System.Security.Cryptography;

namespace MiniStore.Test
{
    public class CouponUnitTest
    {
        private readonly ApplicationDbContext _context;
        private ICouponService _couponService;

        //Test Coupone Services
        [SetUp]
        public void Setup()
        {
            _couponService = new CouponServices(_context);
        }
        [Test]
        public void GetPath_AllUser()
        {
            var result =  _couponService.GetPath();
            Console.WriteLine(result);
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void GetCouponsAsync_AllRolesUser_ResultDataIsTrue()
        {
            var result = _couponService.GetCouponsAsync();
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void GetCouponDetailById_AllRoleUser_ResultIsNotNull()
        {
            var result = _couponService.GetCouponDetailById("");
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void AddCoupon_AllRoleUser_IsTrue()
        {
            var result = _couponService.AddCoupon(new Models.Coupon());
            Assert.That(result, Is.True);
        }
        [Test]
        public void EditCoupon_AllRoleUser_IsTrue()
        {
            var result = _couponService.EditCoupon("",new Models.Coupon());
            Assert.That(result, Is.True);
        }
        [Test]
        public void DeleteCoupon_AllRoleUser_IsTrue()
        {
            var result = _couponService.DeleteCoupon("");
            Assert.That(result, Is.True);
        }
    }
}