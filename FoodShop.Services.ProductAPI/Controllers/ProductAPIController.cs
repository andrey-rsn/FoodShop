using FoodShop.Services.ProductAPI.Models.DTO;
using FoodShop.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDTO();
        }

        [HttpGet]
        public async Task<ResponseDTO> Get()
        {
            try 
            {
                IEnumerable<ProductDTO> productDTOs = await _productRepository.GetProducts();
                _response.Result=productDTOs;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages= new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
