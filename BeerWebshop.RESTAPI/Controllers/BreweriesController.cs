﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.RESTAPI.Services.Interfaces;
using BeerWebshop.RESTAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BreweriesController : ControllerBase
{
    private readonly IBreweryService _breweryService;

    public BreweriesController(IBreweryService breweryService)
    {
        _breweryService = breweryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBreweriesAsync()
    {
        try
        {
            var breweries = await _breweryService.GetBreweriesAsync();
            var breweryDTOs = breweries.Select(category => new BreweryDTO
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            return Ok(breweryDTOs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> CreateBreweryAsync([FromBody] BreweryDTO breweryDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var brewery = MappingHelper.MapBreweryDTOToEntity(breweryDTO);
            var breweryId = await _breweryService.CreateBreweryAsync(brewery);
            breweryDTO.Id = breweryId;
            return Ok(breweryId);

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteBreweryAsync(int id)
    {
        try
        {
            var result = await _breweryService.DeleteBreweryAsync(id);
            if (!result)
            {
                return NotFound($"Brewery with id {id} was not found.");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

