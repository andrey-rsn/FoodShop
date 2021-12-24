namespace FoodShop.Services.OrderAPI.Models
{
    public class UpdatePaymentResultMessage
    {
        public int  OrderId { get; set; }
        public bool Status { get; set; }
    }
}
