using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShop.Services.OrderAPI.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderHeaderId { get; set; }
        [ForeignKey(nameof(OrderHeaderId))]
        public virtual OrderHeader OrderHeader { get; set; }
        public int ProductId { get; set; }
        public  string ProductName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
