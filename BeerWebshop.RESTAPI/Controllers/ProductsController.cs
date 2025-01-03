﻿using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.RESTAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBreweryService _breweryService;
        public ProductsController(IProductService productService, ICategoryService categoryService, IBreweryService breweryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _breweryService = breweryService;
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var productDTO = await _productService.GetProductByIdAsync(id);
                if (productDTO == null)
                {
                    return NotFound($"Product with id {id} was not found.");
                }

                return Ok(productDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var productId = await _productService.CreateProductAsync(productDTO);

                productDTO.Id = productId;

                return CreatedAtRoute("GetProductById", new { id = productId }, productId);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteProductByIdAsync(int id)
        {
            var result = await _productService.DeleteProductByIdAsync(id);
            if (!result)
            {
                return NotFound($"Product with id {id} was not found.");
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromBody] ProductQueryParameters parameters)
        {
            if (parameters.PageSize > 21)
            {
                try
                {
                    var role = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                    if (string.IsNullOrEmpty(role) || !role.Equals("Admin"))
                    {
                        return BadRequest(parameters.PageSize + "is too high");
                    }
                }
                catch
                {
                    return BadRequest(parameters.PageSize + " is too high");

                }
            }

            try
            {
                var productDTOs = await _productService.GetProductsAsync(parameters);
                return Ok(productDTOs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetProductCountFromParameters([FromBody] ProductQueryParameters parameters)
        {
            try
            {
                var result = await _productService.GetProductsCount(parameters);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid || productDTO.Id != id) return BadRequest();

            try
            {
                productDTO.Id = id;
                var result = await _productService.UpdateProductAsync(productDTO);
                if (!result)
                {
                    return NotFound($"Product with id {id} was not found.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}