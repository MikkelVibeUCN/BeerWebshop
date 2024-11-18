using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IBreweryDAO
{
    Task<int> CreateAsync(Brewery brewery);
    Task<bool> DeleteAsync(int id);

    Task<int?> GetBreweryIdByName(string breweryName);
    Task<Brewery?> GetBreweryById(int id);
}
