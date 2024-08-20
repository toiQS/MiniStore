using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MiniStore.API.Models;
using MiniStore.API.Models.coupon;
using MiniStore.Services.coupon;

namespace MiniStore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCoupons()
        {
            try
            {
                var result = await _couponService.GetCouponsAsync();
                if (!result.Any())
                {
                    return Ok(ServiceResult<IEnumerable<CouponModelResponse>>.FailedResult("Not Found or Don't exist", 500));
                }
                var convertData = result.Select(x => new CouponModelResponse()
                {
                    CouponName = x.CouponName,
                    CouponDescription = x.CouponDescription,
                    CouponId = x.CouponId,
                }).ToList();
                return Ok(ServiceResult<IEnumerable<CouponModelResponse>>.SuccessResult(convertData));

            }
            catch (Exception ex) {
                return BadRequest(ServiceResult<IEnumerable<CouponModelResponse>>.FailedResult("There are some extension issues here", 501));
            }
        }
    }
}
