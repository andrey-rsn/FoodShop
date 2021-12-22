namespace FoodShop.Services.ShoppingCartAPI.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);
    }
}
