using FoodShop.Web.Models;
using FoodShop.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodShop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        public CartController(ILogger<HomeController> logger, IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }


        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.ApplyCouponAsync<ResponseDTO>(cartDTO, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveCouponAsync<ResponseDTO>(cartDTO.CartHeader.UserId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDTO>(cartDetailsId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        private async Task<CartDTO> LoadCartDtoBasedOnLoggedInUser()
        {
            var UserId = User.Claims.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserIdAsync<ResponseDTO>(UserId,accessToken);
            CartDTO cartDTO = new();
            if (response != null&& response.IsSuccess)
            {
                cartDTO = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
            }
            if(cartDTO.CartHeader!=null)
            {
                if(!string.IsNullOrEmpty(cartDTO.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon<ResponseDTO>(cartDTO.CartHeader.CouponCode, accessToken);
                    if (coupon.Result != null && coupon.IsSuccess)
                    {
                        var cuponObj = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(coupon.Result));
                        cartDTO.CartHeader.DiscountTotal = cuponObj.DiscountAmount;
                    }
                }
                foreach(var details in cartDTO.CartDetails)
                {
                    cartDTO.CartHeader.OrderTotal += (details.Product.Price * details.Count);
                }
                cartDTO.CartHeader.OrderTotal -= cartDTO.CartHeader.DiscountTotal;
            }
            return cartDTO;
        }
    }
}
