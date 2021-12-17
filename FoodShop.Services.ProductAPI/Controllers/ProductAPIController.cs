using FoodShop.Services.ProductAPI.Models;
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

        [HttpGet]
        [Route ("{id}")]
        public async Task<ResponseDTO> Get(int id)
        {
            try
            {
                ProductDTO productDTO = await _productRepository.GetProductById(id);
                _response.Result = productDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ResponseDTO> Post([FromBody]ProductDTO productDto)
        {
            try
            {
                ProductDTO model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ResponseDTO> Put([FromBody] ProductDTO productDto)
        {
            try
            {
                ProductDTO model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDTO> Delete(int id)
        {
            try
            {
                bool IsSuccess = await _productRepository.DeleteProduct(id);
                _response.Result = IsSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
