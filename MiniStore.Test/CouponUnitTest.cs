using MiniStore.Data;
using MiniStore.Services.coupon;
using System.Security.Cryptography;

namespace MiniStore.Test
{
    public class CouponUnitTest
    {
        private readonly ApplicationDbContext _context;
        private ICouponService _couponService;

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
    }
}