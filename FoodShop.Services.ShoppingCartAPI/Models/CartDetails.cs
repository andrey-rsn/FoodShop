﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodShop.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        [Key]
        public int CartDetailsId { get; set; }

        
        public int? CartHeaderId { get; set; }

        [ForeignKey(nameof(CartHeaderId))]

        public virtual CartHeader? CartHeader { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]

        public virtual Product Product { get; set; }

        public int Count { get; set; }
    }
}
