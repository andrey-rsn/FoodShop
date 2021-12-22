namespace FoodShop.Services.ShoppingCartAPI.MessageBus
{
    public class BaseMessage
    {
        public int Id { get; set; }

        public DateTime MessageCreated { get; set; }
    }
}
