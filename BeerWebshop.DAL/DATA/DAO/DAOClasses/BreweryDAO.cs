using BeerWebshop.DAL.DATA.Entities;
using System.Data.SqlClient;
using Dapper;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class BreweryDAO : IBreweryDAO
{
	private const string _insertBrewerySql = @"INSERT INTO Breweries (Name, Country, IsDeleted)
												VALUES (@Name, @Country, @IsDeleted);
												SELECT CAST(SCOPE_IDENTITY() AS int);";

	private readonly string _connectionString;

    public BreweryDAO(string connectionString)
    {
		_connectionString = connectionString;
	}
    public async Task<int> CreateBreweryAsync(Brewery brewery)
	{
		using var connection = new SqlConnection(_connectionString);
		try
		{
			var parameters = new
			{
				brewery.Name,
				brewery.IsDeleted
			};

			var newBreweryId = await connection.QuerySingleAsync<int>(_insertBrewerySql, parameters);
			return newBreweryId;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error creating brewery: {ex.Message}", ex);
		}
	}
}
