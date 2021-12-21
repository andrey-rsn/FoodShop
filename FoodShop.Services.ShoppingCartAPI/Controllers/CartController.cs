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
        protected ResponseDTO _response;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            this._response = new ResponseDTO();
        }
        
        [HttpGet("GetCart/{UserId}")]
        public async Task<ResponseDTO> GetCart(string UserId)
        {
            try
            {
                CartDTO cartDTO = await _cartRepository.GetCartByUserId(UserId);
                _response.Result= cartDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<ResponseDTO> AddCart(CartDTO cartDto)
        {

            try
            {
                CartDTO cart = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response; 
        }

        [HttpPost("UpdateCart")]
        public async Task<ResponseDTO> UpdateCart(CartDTO cartDto)
        {
            try
            {
                CartDTO cart = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("DeleteCart")]
        public async Task<ResponseDTO> DeleteCart([FromBody]int CartId)
        {
            try
            {
                bool IsSuccess = await _cartRepository.RemoveFromCart(CartId);
                _response.Result = IsSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("ClearCart")]
        public async Task<ResponseDTO> ClearCart([FromBody] string UserId)
        {
            try
            {
                bool IsSuccess = await _cartRepository.ClearCart(UserId);
                _response.Result = IsSuccess;
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
