using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class BeerDao : IBeerDAO
{
    private readonly string _connectionString;

    public BeerDao(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<int> CreateBeerAsync(Beer beer)
    {
        const string sql = @"
        INSERT INTO Products (Name, Brewery, Price, Description, Stock, ABV, Category)
        VALUES (@Name, @Brewery, @Price, @Description, @Stock, @ABV, @Category);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

        using (var connection = new SqlConnection("YourConnectionString"))
        {
            connection.Open();
            var id = await connection.ExecuteScalarAsync<int>(sql, new
            {
                beer.Name,
                beer.Brewery,
                beer.Price,
                beer.Description,
                beer.Stock,
                beer.ABV,
                beer.Category
            });
            return id;
        }
    }

    public Task<Beer> GetBeerByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
