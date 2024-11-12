using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.RESTAPI.Tools;
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

		var brewery = MappingHelper.MapBreweryDTOToEntity(breweryDTO);

		var breweryId = await _breweryDao.CreateBreweryAsync(brewery);

		breweryDTO.Id = breweryId;

		return Ok(breweryId);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBreweryAsync(int id)
	{
		var result = await _breweryDao.DeleteAsync(id);
		if (!result)
		{
			return NotFound($"Brewery with id {id} was not found.");
		}
		return Ok();
	}

}
