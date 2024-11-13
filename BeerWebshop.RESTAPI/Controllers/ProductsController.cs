using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.RESTAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ProductService _productService;
		private readonly CategoryService _categoryService;
		private readonly BreweryService _breweryService;
		public ProductsController(ProductService productService, CategoryService categoryService, BreweryService breweryService)
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
	}
}