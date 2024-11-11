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
        private readonly BeerService _beerService;
        public CartController(ICartService cartService, BeerService beerService)
        {
            _cartService = cartService;
            _beerService = beerService;
        }


        // GET: CartController
        public ActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
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
                ProductDTO? product = await _beerService.GetProductFromId(productId);

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
            ProductDTO? beer = await _beerService.GetProductFromId(productId);
            if(beer == null)
            {
                throw new Exception("Beer not found");
            }
            return _cartService.HasEnoughStock(beer, newQuantity);
        }
    }
}