using FoodShop.Web.Models;

namespace FoodShop.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string UserId, string token );
        Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token );
        Task<T> UpdateCartAsync<T>(CartDTO cartDTO, string token );
        Task<T> RemoveFromCartAsync<T>(int CartId, string token );

    }
}
