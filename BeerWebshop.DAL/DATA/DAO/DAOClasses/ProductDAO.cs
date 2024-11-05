using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class ProductDAO : IProductDAO
{
    private const string _insertProductSql = @"
        INSERT INTO Products (Name, Brewery, Price, Description, Stock, ABV, Category)
        VALUES (@Name, @Brewery, @Price, @Description, @Stock, @ABV, @Category);
        SELECT CAST(SCOPE_IDENTITY() AS int);";

    private const string _getFromCategorySql = @"SELECT * FROM Products WHERE Category = @Category;";

    private const string _getByIdSql = @"SELECT * FROM Products WHERE Id = @Id;";
    private const string _deleteByIdSql = @"DELETE FROM Products WHERE Id = @Id;";
    private const string _getAllSql = @"SELECT * FROM Products;";

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
            var newProductId = await connection.QuerySingleAsync<int>(_insertProductSql, product);
            return newProductId;
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

    public async Task<IEnumerable<Product>> GetFromCategoryAsync(string category)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Product>(_getFromCategorySql, new { Category = category });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting products from category: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Product>(_getAllSql);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting all products: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(_deleteByIdSql, new { Id = id });
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting product by id: {ex.Message}", ex);
        }
    }
}
