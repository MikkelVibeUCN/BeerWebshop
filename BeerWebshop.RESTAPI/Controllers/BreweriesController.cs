using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BreweriesController : ControllerBase
{
	private readonly IBreweryDAO _breweryDao;

	public BreweriesController(IBreweryDAO breweryDao)
	{
		_breweryDao = breweryDao;
	}

	[HttpPost]
	public async Task<IActionResult> CreateBreweryAsync([FromBody] BreweryDTO breweryDTO)
	{
		if (breweryDTO == null)
		{
			return BadRequest();
		}

		var product = await MapToEntity(breweryDTO);

		var productId = await _breweryDao.CreateBreweryAsync(product);

		breweryDTO.Id = productId;

		return Ok();
	}

	private async Task<Brewery> MapToEntity(BreweryDTO breweryDTO)
	{
		return new Brewery
		{
			Name = breweryDTO.Name,
			IsDeleted = false
		};
	}

}
