using FoodShop.Web.Models;
using FoodShop.Web.Services.IServices;

namespace FoodShop.Web.Services
{
    public class CartService :BaseService, ICartService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDTO,
                Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCouponAsync<T>(CartDTO cartDTO, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDTO,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
                AccessToken = token
            });
        }

        public async Task<T> CheckoutAsync<T>(CartHeaderDTO cartHeaderDTO, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartHeaderDTO,
                Url = SD.ShoppingCartAPIBase + "/api/cart/Checkout",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string UserId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + UserId,
                AccessToken = token
            });
        }

        public async Task<T> RemoveCouponAsync<T>(string userId, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int CartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = CartId,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDTO,
                Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
                AccessToken = token
            });
        }
    }
}
