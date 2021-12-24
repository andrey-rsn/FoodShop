using FoodShop.Services.OrderAPI.Models;

namespace FoodShop.Services.OrderAPI.Messaging
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);
    }
}
