using System.ComponentModel.DataAnnotations;

namespace FoodShop.Services.ShoppingCartAPI.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public CartHeader CartHeader { get; set; }

        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
