using AutoMapper;

namespace FoodShop.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
               //config.CreateMap<ProductDTO, Product>();
               //config.CreateMap<Product, ProductDTO>();
            });
            return mappingConfig;
        }
    }
}
