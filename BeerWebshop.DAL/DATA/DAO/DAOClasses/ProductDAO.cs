using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class ProductDAO : IProductDAO
{
    private const string _insertProductSql = @"
        INSERT INTO Products (Name, Brewery, Price, Description, Stock, ABV, Category)
        VALUES (@Name, @Brewery, @Price, @Description, @Stock, @ABV, @Category);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

    private const string _getByIdSql = @"SELECT * FROM Products WHERE Id = @Id;";

    private readonly string _connectionString;

    public ProductDAO(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<int> CreateAsync(Product product)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            return (await connection.QuerySingleAsync<int>(_insertProductSql, product));
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating product: {ex.Message}", ex);
        }
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<Product>(_getByIdSql, new { Id = id });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting product by id: {ex.Message}", ex);
        }

    }
}
