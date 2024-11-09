using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;
using System.Runtime.CompilerServices;

namespace BeerWebshop.RESTAPI.Services
{
    public class BreweryService
    {
        private readonly IBreweryDAO _breweryDAO;
        public BreweryService(IBreweryDAO breweryDAO)
        {
            _breweryDAO = breweryDAO;
        }

        public async Task<int?> GetBreweryIdByName(string name)
        {
            return await _breweryDAO.GetBreweryIdByName(name);
        }

        public async Task<Brewery?> GetBreweryById(int id)
        {
            return await _breweryDAO.GetBreweryById(id);
        }
    }
}
