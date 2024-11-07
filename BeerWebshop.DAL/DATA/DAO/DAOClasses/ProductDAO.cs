using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class ProductDAO : IProductDAO
{
    private const string _insertProductSql = @"
    INSERT INTO Products 
    (Name, CategoryId_FK as CategoryId, BreweryId_FK as BreweryId, Price, Description, Stock, Abv, ImageUrl, IsDeleted)
    VALUES (@Name, @Category, @BreweryId, @Price, @Description, @Stock, @Abv, @ImageUrl, @IsDeleted);
    SELECT CAST(SCOPE_IDENTITY() AS int);";

    private const string _getByIdSql = @"
    SELECT p.*, c.Name AS Category, b.Name AS Brewery 
    FROM Products p
    LEFT JOIN Categories c ON p.CategoryId_FK = c.Id
    LEFT JOIN Breweries b ON p.BreweryId_FK = b.Id
    WHERE p.Id = @Id;";

    private const string _getFromCategorySql = @"
    SELECT p.* 
    FROM Products p
    JOIN Categories c ON p.CategoryId_FK = c.Id
    WHERE c.Name = @Category;";


    private const string _deleteByIdSql = @"DELETE FROM Products WHERE Id = @Id;";

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
            var parameters = new
            {
                product.Name,
                CategoryId = product.Category?.Id,
                BreweryId = product.Brewery?.Id,
                product.Price,
                product.Description,
                product.Stock,
                product.Abv,
                product.ImageUrl,
                product.IsDeleted
            };
            var newProductId = await connection.QuerySingleAsync<int>(_insertProductSql, parameters);
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

    public async Task<bool> DeleteAsync(int id)
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
