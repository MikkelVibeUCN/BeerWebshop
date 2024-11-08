using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly ICategoryDAO _categoryDao;

	public CategoriesController(ICategoryDAO categoryDao)
	{
		_categoryDao = categoryDao;
	}

	[HttpPost]
	public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDTO categoryDTO)
	{
		if (categoryDTO == null)
		{
			return BadRequest("Category data is required.");
		}

		var category = await MapToEntity(categoryDTO);

		var categoryId = await _categoryDao.CreateCategoryAsync(category);

		categoryDTO.Id = categoryId;

		return Ok();
	}

	private async Task<Category> MapToEntity(CategoryDTO categoryDTO)
	{
		return new Category
		{
			Name = categoryDTO.Name,
			IsDeleted = false
		};
	}
}
