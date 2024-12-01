﻿using BeerWebshop.Web.Services;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;
using BeerWebshop.Web.Cookies;
using BeerWebshop.Web.Models;

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
                ProductDTO product = await GetProduct(productId);

                if (!HasEnoughStock(product.Stock, newQuantity)) return BadRequest("Not enough products in stock");

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
                ProductDTO product = await GetProduct(productId);

                _cartService.AddToCart(product, quantity);

                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        private async Task<ProductDTO> GetProduct(int productId)
        {
            ProductDTO? product = await _productService.GetProductFromId(productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        private bool HasEnoughStock(int productStock, int newQuantity)
        {
            return productStock >= newQuantity;
        }
    }
}