namespace FoodShop.Web.Models
{
    public class CartHeaderDTO
    {
        public int CartHeaderId { get; set; }

        public string UserId { get; set; }

        public string CouponCode { get; set; }

        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PickUpDateTime { get; set; }

        public string Phone { get; set; }

        public string EMail { get; set; }

        public string CardNumber { get; set; }

        public string CVV { get; set; }

        public string ExpiryMonthYear { get; set; }
    }
}
