using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly CategoryService _categoryService;

	public CategoriesController(CategoryService categoryService)
	{
        _categoryService = categoryService;
    }

	[HttpPost]
	public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDTO categoryDTO)
	{
		if (categoryDTO == null)
		{
			return BadRequest("Category data is required.");
		}

		var category = MapToEntity(categoryDTO);

		var categoryId = await _categoryService.CreateCategoryAsync(category);

		categoryDTO.Id = categoryId;

		return Ok();
	}

	private static Category MapToEntity(CategoryDTO categoryDTO)
	{
		return new Category
		{
			Name = categoryDTO.Name,
			IsDeleted = false
		};
	}

	[HttpGet]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        var categories = await _categoryService.GetAlLCategories();
        var categoryDTOs = categories.Select(category => new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        }).ToList();

        return Ok(categoryDTOs);
    }
}