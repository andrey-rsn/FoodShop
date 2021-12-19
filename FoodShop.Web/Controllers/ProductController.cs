using FoodShop.Web.Models;
using FoodShop.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> list = new List<ProductDTO>();
            var response = await _productService.GetAllProductsAsync<ResponseDTO>();
            if(response!= null && response.IsSuccess)
            {
                list=JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [HttpGet]

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDTO>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.GetProductByIdAsync<ResponseDTO>(productId);
                if (response != null && response.IsSuccess)
                {
                    ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDTO>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.GetProductByIdAsync<ResponseDTO>(productId);
                if (response != null && response.IsSuccess)
                {
                    ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDTO model)
        {
            if (model.ProductId!=null)
            {
                var response = await _productService.DeleteProductAsync<ResponseDTO>(model.ProductId);
                if ( response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);

        }
    }
}
