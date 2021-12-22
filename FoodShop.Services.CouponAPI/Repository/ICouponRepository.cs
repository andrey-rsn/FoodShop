using FoodShop.Services.CouponAPI.Models.DTO;

namespace FoodShop.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetCouponByCode(string couponCode);
    }
}
