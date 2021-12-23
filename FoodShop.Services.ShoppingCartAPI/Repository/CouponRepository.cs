using FoodShop.Services.ShoppingCartAPI.Models.Dto;
using FoodShop.Services.ShoppingCartAPI.Models.DTO;
using Newtonsoft.Json;

namespace FoodShop.Services.ShoppingCartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _httpClient;

        public CouponRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CouponDTO> GetCoupon(string couponName)
        {
            var response = await _httpClient.GetAsync($"api/coupon/{couponName}");
            var apiContent= await response.Content.ReadAsStringAsync();
            var resp=JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
            if(resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(resp.Result));
            }
            return new CouponDTO();
        }
    }
}
