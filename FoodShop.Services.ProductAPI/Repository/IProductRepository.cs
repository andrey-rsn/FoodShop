using FoodShop.Services.ProductAPI.Models.DTO;

namespace FoodShop.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();

        Task<ProductDTO> GetProductById(int ProductId);

        Task<ProductDTO> CreateUpdateProduct (ProductDTO productDTO);

        Task<bool> DeleteProduct (int ProductId);
    }
}
