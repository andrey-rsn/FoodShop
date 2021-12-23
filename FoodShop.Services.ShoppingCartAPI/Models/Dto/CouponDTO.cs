namespace FoodShop.Services.ShoppingCartAPI.Models.Dto
{
    public class CouponDTO
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; }

        public double DiscountAmount { get; set; }
    }
}
