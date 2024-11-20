using BeerWebshop.DAL.DATA.Entities;
using System.Data.SqlClient;
using Dapper;
using BeerWebshop.DAL.DATA.DAO.Interfaces;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class BreweryDAO : IBreweryDAO
{
    #region Sql Query
    private const string InsertBrewerySql = @"INSERT INTO Breweries (Name, IsDeleted) VALUES (@Name, @IsDeleted) SELECT CAST(SCOPE_IDENTITY() AS int);";
	private const string DeleteBrewerySql = @"DELETE FROM Breweries WHERE Id = @Id";
	private const string GetBreweryByIdSql = @"SELECT * FROM Breweries WHERE Id = @Id;";
	private const string GetBreweryIdByNameSql = @"SELECT Id FROM Breweries WHERE Name = @Name AND IsDeleted = 0";
	private const string GetAllBreweriesSql = @"SELECT * FROM Breweries";

    #endregion

    #region Dependency injection
    private readonly string _connectionString;
    public BreweryDAO(string connectionString)
	{
		_connectionString = connectionString;
	}
    #endregion

    #region BaseDAO methods
    public async Task<int> CreateAsync(Brewery brewery)
	{
		using var connection = new SqlConnection(_connectionString);
		try
		{
			var parameters = new
			{
				brewery.Name,
				brewery.IsDeleted
			};

			var newBreweryId = await connection.QuerySingleAsync<int>(InsertBrewerySql, parameters);
			return newBreweryId;
		}
        catch (SqlException sqlEx) when (sqlEx.Number == 2627 || sqlEx.Number == 2601) //Sql unique constraint violation
        {
            throw new InvalidOperationException($"Brewery with name '{brewery.Name}' already exists.", sqlEx);
        }
    }
    public async Task<Brewery?> GetByIdAsync(int breweryId)
    {
        using var connection = new SqlConnection(_connectionString);
        try
        {
            return await connection.QuerySingleOrDefaultAsync<Brewery>(GetBreweryByIdSql, new { Id = breweryId });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving brewery with Id: {breweryId}: {ex.Message}", ex);
        }
        
    }

	public async Task<bool> DeleteAsync(int id)
	{
		using var connection = new SqlConnection(_connectionString);
		try
		{
			var parameters = new
			{
				Id = id
			};

			var result = await connection.ExecuteAsync(DeleteBrewerySql, parameters);
			return result > 0;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error deleting brewery: {ex.Message}", ex);
		}
	}
	public async Task<IEnumerable<Brewery>> GetAllAsync()
	{
		using var connection = new SqlConnection(_connectionString);
		try
		{
			// Fetch all breweries and return them as an IEnumerable
			return await connection.QueryAsync<Brewery>(GetAllBreweriesSql);
		}
		catch (Exception ex)
		{
			throw new Exception($"Error retrieving all breweries: {ex.Message}", ex);
		}
	}

    public Task<bool> UpdateAsync(Brewery entity)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region IBreweryDAO methods
	public async Task<int?> GetBreweryIdByName(string breweryName)
	{
		using var connection = new SqlConnection(_connectionString);
		try
		{
			return await connection.QuerySingleOrDefaultAsync<int?>(GetBreweryIdByNameSql, new { Name = breweryName });
		}
		catch (Exception ex)
		{
			throw new Exception($"Error retrieving brewery with: {breweryName}: {ex.Message}", ex);
		}
	}
}
#endregion
