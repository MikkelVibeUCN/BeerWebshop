﻿using BeerWebshop.DAL.DATA.Entities;
using System.Data.SqlClient;
using Dapper;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class BreweryDAO : IBreweryDAO
{
	private const string _insertBrewerySql = @"INSERT INTO Breweries (Name, IsDeleted)
												VALUES (@Name, @IsDeleted);
												SELECT CAST(SCOPE_IDENTITY() AS int);";

	private const string _softDeleteBrewerySql = @"UPDATE Breweries
												SET IsDeleted = 1
												WHERE Id = @Id";
	private const string _deleteBrewerySql = @"DELETE FROM Breweries
												WHERE Id = @Id";

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

	public async Task<bool> DeleteAsync(int id)
	{
		using var connection = new SqlConnection(_connectionString);
		try
		{
			var parameters = new
			{
				Id = id
			};

			var result = await connection.ExecuteAsync(_deleteBrewerySql, parameters);
			return result > 0;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error deleting brewery: {ex.Message}", ex);
		}
	}


    public async Task<Brewery?> GetBreweryById(int breweryId)
    {
        const string sql = "SELECT * FROM Breweries WHERE Id = @Id;";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<Brewery>(sql, new { Id = breweryId });
    }

    public async Task<int?> GetBreweryIdByName(string breweryName)
    {
        const string sql = "SELECT Id FROM Breweries WHERE Name = @Name AND IsDeleted = 0";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<int?>(sql, new { Name = breweryName });
    }
}
