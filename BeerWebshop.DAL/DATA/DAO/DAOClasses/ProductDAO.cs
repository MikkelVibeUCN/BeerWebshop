using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class ProductDAO : IProductDAO
{
    #region Sql query
    private const string InsertProductSql = @"INSERT INTO Products (Name, CategoryId_FK, BreweryId_FK, Price, Description, Stock, Abv, ImageUrl, IsDeleted)
        VALUES (@Name, @CategoryId, @BreweryId, @Price, @Description, @Stock, @Abv, @ImageUrl, @IsDeleted);
        SELECT CAST(SCOPE_IDENTITY() AS int);";
	private const string GetByIdSql = @"
                SELECT p.Id, p.Name, p.Description, p.ImageUrl, p.Price, p.Stock, p.Abv, p.RowVersion, p.IsDeleted, 
                       c.Id as Id, c.Name AS Name, 
                       b.Id as Id, b.Name AS Name
                FROM Products p
                INNER JOIN Categories c ON p.CategoryId_FK = c.Id
                INNER JOIN Breweries b ON p.BreweryId_FK = b.Id                
                WHERE p.IsDeleted = 0 AND p.Id = @Id";
	private const string DeleteByIdSql = @"DELETE FROM Products WHERE Id = @Id;";
	private const string UpdateByIdSql = @"UPDATE Products SET Name = @Name, CategoryId_FK = @CategoryId, BreweryId_FK = @BreweryId, 
											Price = @Price, Description = @Description, Stock = @Stock, Abv = @Abv, ImageUrl = @ImageUrl, 
											IsDeleted = @IsDeleted WHERE Id = @Id and RowVersion = @RowVersion;";

	private const string GetFromCategorySql = @"SELECT p.* FROM Products p JOIN Categories c ON p.CategoryId_FK = c.Id WHERE c.Name = @Category;";
	private const string GetAllProductCategoriesSql = @"SELECT Name FROM Categories WHERE IsDeleted = 0;";
	private const string BaseProductSql = @"
        SELECT p.Id, p.Name, p.Description, p.ImageUrl, p.Price, p.ABV, p.Stock, p.Rowversion,
               c.Id AS Id, c.Name AS Name, 
               b.Id AS Id, b.Name AS Name
        FROM Products p
        INNER JOIN Breweries b ON p.BreweryId_FK = b.Id
        INNER JOIN Categories c ON p.CategoryId_FK = c.Id
        WHERE p.IsDeleted = 0 AND p.Stock > 0";
	private const string GetProductCountSql = @"
            SELECT COUNT(*) 
            FROM Products p
            INNER JOIN Breweries b ON p.BreweryId_FK = b.Id
            INNER JOIN Categories c ON p.CategoryId_FK = c.Id
            WHERE p.IsDeleted = 0";
    #endregion

    #region Dependency injection
    private readonly string _connectionString;
	public ProductDAO(string connectionString)
	{
		_connectionString = connectionString;
	}
    #endregion

    #region BaseDAO Methods
    public async Task<int> CreateAsync(Product product, SqlConnection? connection, DbTransaction? transaction)
	{
		try
		{
			connection = new SqlConnection(_connectionString);
			var parameters = new
			{
				product.Name,
				CategoryId = product.Category.Id,
				BreweryId = product.Brewery.Id,
				product.Price,
				product.Description,
				product.Stock,
				product.Abv,
				product.ImageUrl,
				product.IsDeleted
			};
			var newProductId = await connection.QuerySingleAsync<int>(InsertProductSql, parameters);
			return newProductId;
		}
		catch (SqlException sqlEx) when (sqlEx.Number == 2627 || sqlEx.Number == 2601) 
		{
			throw new InvalidOperationException($"A product with the name '{product.Name}' already exists.", sqlEx);
		}
		catch (Exception ex)
		{
			throw new Exception($"Error creating product: {ex.Message}", ex);
		}
	}
	public async Task<Product?> GetByIdAsync(int id)
	{
		try
		{
			using var connection = new SqlConnection(_connectionString);

			var result = await connection.QueryAsync<Product, Category, Brewery, Product>(
				GetByIdSql,
				(product, category, brewery) =>
				{
					product.Category = category;
					product.Brewery = brewery;

					return product;
				},
				param: new { Id = id },
				splitOn: "Id,Id"
			);
			return result.FirstOrDefault();
		}
		catch (Exception ex)
		{
			throw new Exception($"Error getting product by id: {ex.Message}", ex);
		}
	}
	public async Task<bool> UpdateAsync(Product product)
	{

		try
		{
			using var connection = new SqlConnection(_connectionString);
			var rowsAffected = await connection.ExecuteAsync(UpdateByIdSql, new
			{
				product.Name,
				CategoryId = product.Category.Id,
				BreweryId = product.Brewery.Id,
				product.Price,
				product.Description,
				product.Stock,
				product.Abv,
				product.ImageUrl,
				product.IsDeleted,
				Id = product.Id,
				product.RowVersion
			});

			if(rowsAffected == 0)
			{
				throw new Exception("Concurrency conflict detected.");
			}

			return true;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error updating product: {ex.Message}", ex);
		}
	}

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);

        try
        {
            var rowsAffected = await connection.ExecuteAsync(DeleteByIdSql, new { Id = id });
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting product by id: {ex.Message}", ex);
        }
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region IProductDAO Methods
    //TODO Timeout ved deadlock.
  

	public async Task<IEnumerable<string>> GetProductCategoriesAsync()
	{
		using (var connection = new SqlConnection(_connectionString))
		{

			try
			{
				return await connection.QueryAsync<string>(GetAllProductCategoriesSql);
			}
			catch (Exception ex)
			{

				throw new Exception($"Error retrieving categories: {ex.Message}", ex);
			}
		}
	}

	public async Task<IEnumerable<Product>> GetFromCategoryAsync(string category)
	{
		try
		{
			using var connection = new SqlConnection(_connectionString);
			return await connection.QueryAsync<Product>(GetFromCategorySql, new { Category = category });
		}
		catch (Exception ex)
		{
			throw new Exception($"Error getting products from category: {ex.Message}", ex);
		}
	}

	

	// Ny implementering af getproducts som kan tage søgekriterier med
	public async Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters)
	{
		IEnumerable<Product> products = new List<Product>();

		StringBuilder queryBuilder = new StringBuilder(BaseProductSql);

		if (!string.IsNullOrEmpty(parameters.Category))
		{
			queryBuilder.Append(" AND c.Name IN (@CategoryNames)");
		}

		// Sortering logik
		if (!string.IsNullOrEmpty(parameters.SortBy))
		{
			string sortColumn = parameters.SortBy.ToLower() switch
			{
				var s when s.StartsWith("price") => "p.Price",
				var s when s.StartsWith("name") => "p.Name",
				_ => "p.Id"
			};
			string sortDirection = parameters.SortBy.EndsWith("Desc", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
			queryBuilder.Append($" ORDER BY {sortColumn} {sortDirection}");
		}
		else
		{
			queryBuilder.Append(" ORDER BY p.Id");
		}

		queryBuilder.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

		string categoryNames = parameters.Category != null ? string.Join(",", parameters.Category) : string.Empty;
		int offset = parameters.PageNumber * parameters.PageSize;
		int pageSize = parameters.PageSize;

		using var connection = new SqlConnection(_connectionString);

		products = await connection.QueryAsync<Product, Category, Brewery, Product>(
			queryBuilder.ToString(),
			(product, category, brewery) =>
			{
				product.Brewery = brewery;
				product.Category = category;
				return product;
			},
			param: new { Offset = offset, PageSize = pageSize, CategoryNames = categoryNames },
			splitOn: "Id,Id"
		);
		return products;
	}

	public async Task<int> GetProductCountAsync(ProductQueryParameters parameters)
	{
		int productCount = 0;

		StringBuilder queryBuilder = new StringBuilder(GetProductCountSql);

		if (!string.IsNullOrEmpty(parameters.Category))
		{
			queryBuilder.Append(" AND c.Name IN (@CategoryNames)");
		}

		try
		{
			using var connection = new SqlConnection(_connectionString);

			productCount = await connection.ExecuteScalarAsync<int>(
				queryBuilder.ToString(),
				new { CategoryNames = parameters.Category != null ? string.Join(",", parameters.Category) : string.Empty }
			);
		}
		catch (Exception ex)
		{

			throw new Exception("An error occurred while retrieving the product count.", ex);
		}
		return productCount;
	}

    public async Task<int> CreateAsync(Product entity)
    {
        using var connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();
		var transaction = await connection.BeginTransactionAsync();
		return await CreateAsync(entity, connection, transaction);

    }
}
#endregion