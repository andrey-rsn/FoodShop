using FoodShop.Web.Models;
using FoodShop.Web.Services.IServices;

namespace FoodShop.Web.Services
{
    public class CouponService :BaseService, ICouponService
    {
        public IHttpClientFactory httpClient { get; set; }

        public CouponService(IHttpClientFactory httpClient):base(httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> GetCoupon<T>(string couponCode, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
