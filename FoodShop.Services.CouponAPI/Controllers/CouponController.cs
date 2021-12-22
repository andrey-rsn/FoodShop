using FoodShop.Services.CouponAPI.Models.DTO;
using FoodShop.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        protected ResponseDTO _response;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository=couponRepository;
            this._response = new ResponseDTO();
        }

        [HttpGet("{couponCode}")]
        public async Task<object> GetDiscountForCode(string couponCode)
        {
            try
            {
                CouponDTO couponDto = await _couponRepository.GetCouponByCode(couponCode);
                _response.Result = couponDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
