using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services
{
	public class BreweryService
	{
		private readonly IBreweryDAO _breweryDAO;
		public BreweryService(IBreweryDAO breweryDAO)
		{
			_breweryDAO = breweryDAO;
		}
		public async Task<int> CreateBreweryAsync(Brewery brewery)
		{
			return await _breweryDAO.CreateBreweryAsync(brewery);
		}
		public async Task<int?> GetBreweryIdByName(string name)
		{
			return await _breweryDAO.GetBreweryIdByName(name);
		}

		public async Task<Brewery?> GetBreweryById(int id)
		{
			return await _breweryDAO.GetBreweryById(id);
		}

		public async Task<bool> DeleteBreweryAsync(int id)
		{
			return await _breweryDAO.DeleteAsync(id);
		}
	}
}
