using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class ProductDAO : IProductDAO
{
    private const string _insertProductSql = @"
    INSERT INTO Products 
    (Name, CategoryId_FK, BreweryId_FK, Price, Description, Stock, Abv, ImageUrl, IsDeleted)
    VALUES (@Name, @CategoryId, @BreweryId, @Price, @Description, @Stock, @Abv, @ImageUrl, @IsDeleted);
    SELECT CAST(SCOPE_IDENTITY() AS int);";

    private const string _getByIdSql = @"
    SELECT * 
    FROM Products 
    WHERE Id = @Id;";

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
                CategoryId = product.CategoryId_FK,
                BreweryId = product.BreweryId_FK,
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
            var product = await connection.QuerySingleOrDefaultAsync<Product>(_getByIdSql, new { Id = id });

            if (product != null)
            {
                if (product.CategoryId_FK.HasValue)
                {
                    product.Category = await GetCategoryByIdAsync(product.CategoryId_FK.Value);
                }

                if (product.BreweryId_FK.HasValue)
                {
                    product.Brewery = await GetBreweryByIdAsync(product.BreweryId_FK.Value);
                }
            }

            return product;
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

    private async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        const string sql = "SELECT * FROM Categories WHERE Id = @Id;";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = categoryId });
    }

    private async Task<Brewery?> GetBreweryByIdAsync(int breweryId)
    {
        const string sql = "SELECT * FROM Breweries WHERE Id = @Id;";
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<Brewery>(sql, new { Id = breweryId });
    }
}
