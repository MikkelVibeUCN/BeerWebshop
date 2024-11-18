using BeerWebshop.APIClientLibrary;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(ProductQueryParameters parameters)
        {
            ViewBag.Categories = await _productService.GetProductCategories();

            int totalProductCount = await _productService.GetProductCount(parameters);
            int totalPages = (int)Math.Ceiling(totalProductCount / (double)parameters.PageSize);

            if(totalPages > 1)
            {
                totalPages--;
            }

            ViewBag.CurrentSortOrder = parameters.SortBy;
            ViewBag.CurrentCategory = parameters.Category;
            ViewBag.CurrentPage = parameters.PageNumber;
            ViewBag.TotalPages = totalPages;

            IEnumerable<ProductDTO> products = await _productService.GetProducts(parameters);

            return View(products);
        }

        public ActionResult Details(int id)
        {
            ProductDTO? product = _productService.GetProductFromId(id).Result;
            if(product != null)
            {
                return View(product);
            }
            return BadRequest();
        }
    }
}