using FoodShop.Services.OrderAPI.Models.Dto;

namespace FoodShop.Services.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeaderDTO orderHeaderDTO);
        Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
    }
}
