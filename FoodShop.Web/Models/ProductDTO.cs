﻿using System.ComponentModel.DataAnnotations;

namespace FoodShop.Web.Models
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            Count = 1;
        }
        public int ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        [Range(1,100)]
        public int Count { get; set; }
    }
}
