using FoodShop.Services.ShoppingCartAPI.MessageBus;
using FoodShop.Services.ShoppingCartAPI.Messages;
using FoodShop.Services.ShoppingCartAPI.Models.Dto;
using FoodShop.Services.ShoppingCartAPI.Models.DTO;
using FoodShop.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Mvc;


namespace FoodShop.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IMessageBus _messageBus;
        protected ResponseDTO _response;

        public CartController(ICartRepository cartRepository, IMessageBus messageBus, ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository;
            this._response = new ResponseDTO();
            _messageBus = messageBus;
            _couponRepository = couponRepository;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDTO cartDto = await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        
        
        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDTO cartDto)
        {
            try
            {
                CartDTO cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDTO cartDto)
        {
            try
            {
                CartDTO cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cartDt;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDTO cartDTO)
        {
            try
            {
                bool isSuccess = await _cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId,cartDTO.CartHeader.CouponCode);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDTO checkoutHeaderDTO)
        {
            try
            {
                CartDTO cartDTO = await _cartRepository.GetCartByUserId(checkoutHeaderDTO.UserId);
                if(cartDTO==null)
                {
                    return BadRequest();
                }

                if(!string.IsNullOrEmpty(checkoutHeaderDTO.CouponCode))
                {
                    CouponDTO couponDTO = await _couponRepository.GetCoupon(checkoutHeaderDTO.CouponCode); 
                    if(couponDTO==null)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>()
                        {
                            "Купон недейстивтелен"
                        };
                        _response.DisplayMessage = "Купон недейстивтелен";
                        return _response;
                    }
                    else if(checkoutHeaderDTO.DiscountTotal!=couponDTO.DiscountAmount)
                    {
                        _response.IsSuccess = false;
                        _response.ErrorMessages = new List<string>()
                        {
                            "Купон недейстивтелен"
                        };
                        _response.DisplayMessage = "Купон недейстивтелен";
                        return _response;
                    }

                }
                checkoutHeaderDTO.CartDetails = cartDTO.CartDetails;
                await _messageBus.PublishMessage(checkoutHeaderDTO, "checkoutmessagetopic");
                await _cartRepository.ClearCart(checkoutHeaderDTO.UserId);
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
