using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IBreweryDAO : IBaseDAO<Brewery>
{
   Task<int?> GetBreweryIdByName(string breweryName);
}
