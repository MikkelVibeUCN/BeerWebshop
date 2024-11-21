using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Tools;
using System.Data.Common;
using System.Data.SqlClient;

namespace BeerWebshop.RESTAPI.Services;

public class ProductService
{
	private readonly IProductDAO _productDAO;
	private readonly CategoryService _categoryService;
	private readonly BreweryService _breweryService;
	public ProductService(IProductDAO productDAO, CategoryService categoryService, BreweryService breweryService)
	{
		_productDAO = productDAO;
		_categoryService = categoryService;
		_breweryService = breweryService;
	}

	public async Task<ProductDTO> GetProductByIdAsync(int id)
	{
		var product = await _productDAO.GetByIdAsync(id);
		if (product == null) return null;

		return MappingHelper.MapProductEntityToDTO(product);
	}

	public async Task<Product?> GetProductEntityByIdAsync(int id)
	{
		return await _productDAO.GetByIdAsync(id);
	}

	public async Task<int> CreateProductAsync(ProductDTO productDTO)
	{
		var categoryId = await _categoryService.GetCategoryIdByName(productDTO.CategoryName);
		var category = await _categoryService.GetCategoryById(categoryId!.Value);

		var breweryId = await _breweryService.GetBreweryIdByName(productDTO.BreweryName);
		var brewery = await _breweryService.GetBreweryById(breweryId!.Value);

		var product = MappingHelper.MapProductDTOToEntity(productDTO, category, brewery);

		return await _productDAO.CreateAsync(product);
	}

	public async Task<List<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters)
	{
		var products = await _productDAO.GetProducts(parameters);

		return products.Select(MappingHelper.MapProductEntityToDTO).ToList();
	}

	public async Task<bool> UpdateStockAsync(int productId, int quantity, SqlConnection? connection = null, DbTransaction? transaction = null)
	{
		return await _productDAO.UpdateStockAsync(productId, quantity, connection, transaction);
	}

	public async Task<int> GetProductsCount(ProductQueryParameters parameters)
	{
		return await _productDAO.GetProductCountAsync(parameters);
	}

	public async Task<bool> UpdateProductAsync(ProductDTO productDTO)
	{
		var categoryId = await _categoryService.GetCategoryIdByName(productDTO.CategoryName);
		var category = await _categoryService.GetCategoryById(categoryId!.Value);
		var breweryId = await _breweryService.GetBreweryIdByName(productDTO.BreweryName);
		var brewery = await _breweryService.GetBreweryById(breweryId!.Value);

		var product = MappingHelper.MapProductDTOToEntity(productDTO, category, brewery);

		return await _productDAO.UpdateAsync(product);


	}


	public async Task<bool> DeleteProductByIdAsync(int id)
	{
		return await _productDAO.DeleteAsync(id);
	}
}
