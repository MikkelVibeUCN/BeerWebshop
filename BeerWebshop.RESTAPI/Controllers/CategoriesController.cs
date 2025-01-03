﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.RESTAPI.Services.Interfaces;
using BeerWebshop.RESTAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDTO categoryDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Category data is required.");
        }

        try
        {
            var category = MappingHelper.MapCategoryDTOToEntity(categoryDTO);

            var categoryId = await _categoryService.CreateCategoryAsync(category);

            categoryDTO.Id = categoryId;

            return Ok(categoryId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        try
        {
            var categories = await _categoryService.GetAlLCategories();
            var categoryDTOs = categories.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return Ok(categoryDTOs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}