using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class ProductDAO : IProductDAO
{
    private const string _insertProductSql = @"INSERT INTO Products (Name, CategoryId_FK, BreweryId_FK, Price, Description, Stock, Abv, ImageUrl, IsDeleted)
                                            VALUES (@Name, @CategoryId, @BreweryId, @Price, @Description, @Stock, @Abv, @ImageUrl, @IsDeleted);
                                            SELECT CAST(SCOPE_IDENTITY() AS int);";

    private const string _getByIdSql = @"SELECT * FROM Products WHERE Id = @Id;";

    private const string _getFromCategorySql = @"SELECT p.* FROM Products p JOIN Categories c ON p.CategoryId_FK = c.Id WHERE c.Name = @Category;";

    private const string _deleteByIdSql = @"DELETE FROM Products WHERE Id = @Id;";

    private const string _getAllProductCategoriesSql = @"SELECT Name FROM Categories WHERE IsDeleted = 0;";

	private const string _updateStockSql = @"
											UPDATE PRODUCTS 
											SET Stock = Stock - @Quantity 
											WHERE Id = @ProductId";

    //TODO Update til køb + inventory management.


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
                CategoryId = product.Category.Id,
                BreweryId = product.Brewery.Id,
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

	public async Task EditAsync(int id)
    {
        throw new NotImplementedException();
    }

	public async Task<Product?> GetByIdAsync(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);

            StringBuilder queryBuilder = new StringBuilder(@"
                SELECT p.Id, p.Name, p.Description, p.ImageUrl, p.Price, p.Stock, p.Abv, p.RowVersion, p.IsDeleted, 
                       c.Id as Id, c.Name AS Name, 
                       b.Id as Id, b.Name AS Name
                FROM Products p
                INNER JOIN Categories c ON p.CategoryId_FK = c.Id
                INNER JOIN Breweries b ON p.BreweryId_FK = b.Id                
                WHERE p.IsDeleted = 0 AND p.Id = @Id");

            var result = await connection.QueryAsync<Product, Category, Brewery, Product>(
                queryBuilder.ToString(),
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

	public async Task<bool> UpdateStockAsync(int productId, int quantity) //TODO Timeout ved deadlock.
	{
		using var connection = new SqlConnection(_connectionString);
		await connection.OpenAsync();

		using var transaction = connection.BeginTransaction();

		var parameters = new DynamicParameters();
		parameters.Add("@ProductId", productId);
		parameters.Add("@Quantity", quantity);

        var stock = await connection.QuerySingleAsync<int>("SELECT Stock FROM Products WHERE Id = @ProductId", new { ProductId = productId }, transaction);
        if(stock < quantity)
		{
			transaction.Rollback();
			throw new InvalidOperationException("Insufficient stock.");
		}

		var rowsAffected = await connection.ExecuteAsync(_updateStockSql, parameters, transaction);
        if(rowsAffected < 0) 
        {
			transaction.Rollback();
			throw new InvalidOperationException("Error updating stock.");
		}

		transaction.Commit();
		return true;
	}

    public async Task<IEnumerable<string>> GetProductCategoriesAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {

            try
            {
                return await connection.QueryAsync<string>(_getAllProductCategoriesSql);
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



    // Ny implementering af getproducts som kan tage søgekriterier med
    public async Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters)
    {
        IEnumerable<Product> products = new List<Product>();

        StringBuilder queryBuilder = new StringBuilder(@"
            SELECT p.Id, p.Name, p.Description, p.ImageUrl, p.Price, 
                   c.Id AS CategoryId, c.Name AS Name, 
                   b.Id AS BreweryId, b.Name AS Name
            FROM Products p
            INNER JOIN Breweries b ON p.BreweryId_FK = b.Id
            INNER JOIN Categories c ON p.CategoryId_FK = c.Id
            WHERE p.IsDeleted = 0 AND p.Stock > 0");

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

        products = await connection.QueryAsync<Product, Brewery, Category, Product>(
            queryBuilder.ToString(),
            (product, brewery, category) =>
            {
                product.Brewery = brewery;
                product.Category = category;
                return product;
            },
            param: new { Offset = offset, PageSize = pageSize, CategoryNames = categoryNames },
            splitOn: "CategoryId,BreweryId"
        );

        return products;
    }



    public async Task<int> GetProductCountAsync(ProductQueryParameters parameters)
    {
        // Initialize count variable
        int count = 0;

        // Build the query for counting the total products based on parameters
        StringBuilder queryBuilder = new StringBuilder(@"
            SELECT COUNT(*) 
            FROM Products p
            INNER JOIN Breweries b ON p.BreweryId_FK = b.Id
            INNER JOIN Categories c ON p.CategoryId_FK = c.Id
            WHERE p.IsDeleted = 0");

        // Apply category filter if specified
        if (!string.IsNullOrEmpty(parameters.Category))
        {
            queryBuilder.Append(" AND c.Name IN (@CategoryNames)");
        }

        // Execute the count query using Dapper
        using var connection = new SqlConnection(_connectionString);

        // Execute the query and return the count
        count = await connection.ExecuteScalarAsync<int>(
            queryBuilder.ToString(),
            new { CategoryNames = parameters.Category != null ? string.Join(",", parameters.Category) : string.Empty }
        );
        return count;
    }
}