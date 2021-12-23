using FoodShop.Services.ShoppingCartAPI.Models.Dto;

namespace FoodShop.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCoupon(string couponName);
    }
}
