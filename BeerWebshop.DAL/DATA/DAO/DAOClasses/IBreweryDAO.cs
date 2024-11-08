using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public interface IBreweryDAO
{
	Task<int> CreateBreweryAsync(Brewery brewery);
}
