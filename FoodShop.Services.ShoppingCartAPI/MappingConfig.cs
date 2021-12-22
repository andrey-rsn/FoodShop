using AutoMapper;
using FoodShop.Services.ShoppingCartAPI.Models;
using FoodShop.Services.ShoppingCartAPI.Models.DTO;

namespace FoodShop.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
               config.CreateMap<ProductDTO, Product>().ReverseMap();
               config.CreateMap<CartDTO, Cart>().ReverseMap();
               config.CreateMap<CartDetailsDTO, CartDetails>().ReverseMap();
               config.CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
