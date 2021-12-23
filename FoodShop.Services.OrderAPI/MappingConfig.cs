using AutoMapper;
using FoodShop.Services.OrderAPI.Models;
using FoodShop.Services.OrderAPI.Models.Dto;

namespace FoodShop.Services.OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderDetailsDTO, OrderDetails>().ReverseMap();
                config.CreateMap<OrderHeaderDTO, OrderHeader>().ReverseMap();
                config.CreateMap<CheckoutHeaderDTO, OrderHeader>().ReverseMap();
                config.CreateMap<CheckoutHeaderDTO, OrderDetails>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
