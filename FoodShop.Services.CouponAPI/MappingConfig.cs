using AutoMapper;
using FoodShop.Services.CouponAPI.Models;
using FoodShop.Services.CouponAPI.Models.DTO;

namespace FoodShop.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>().ReverseMap();
                //config.CreateMap<CartDTO, Cart>().ReverseMap();
                //config.CreateMap<CartDetailsDTO, CartDetails>().ReverseMap();
                //config.CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
