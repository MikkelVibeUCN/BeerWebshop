using BeerWebshop.Web.Services;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

namespace BeerWebshop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ProductService _productService;
        public CartController(ICartService cartService, ProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }


        // GET: CartController
        public ActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public IActionResult RemoveOrderLine(int productId)
        {
            try
            {
                _cartService.RemoveFromCart(productId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int newQuantity)
        {
            try
            {
                if (!await HasEnoughStock(productId, newQuantity))
                {
                    return BadRequest("Not enough products in stock");
                }

                _cartService.UpdateQuantity(productId, newQuantity);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            try
            {
                ProductDTO? product = await _productService.GetProductFromId(productId);

                if(product == null)
                {
                    throw new Exception("Product not found");
                }

                _cartService.AddToCart(product, quantity);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        private async Task<bool> HasEnoughStock(int productId, int newQuantity)
        {
            ProductDTO? product = await _productService.GetProductFromId(productId);
            if(product == null)
            {
                throw new Exception("product not found");
            }
            return _cartService.HasEnoughStock(product, newQuantity);
        }
    }
}