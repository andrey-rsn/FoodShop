﻿using AutoMapper;
using FoodShop.Services.ProductAPI.Models;
using FoodShop.Services.ProductAPI.Models.DTO;

namespace FoodShop.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
             {
                 config.CreateMap<ProductDTO, Product>();
                 config.CreateMap<Product, ProductDTO>();
             });
            return mappingConfig;
        }
    }
}
