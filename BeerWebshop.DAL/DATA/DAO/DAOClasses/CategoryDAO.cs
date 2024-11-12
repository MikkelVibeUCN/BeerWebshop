using BeerWebshop.DAL.DATA.Entities;
using System.Data.SqlClient;
using Dapper;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class CategoryDAO : ICategoryDAO
{
	private const string InsertCategorySql = @"INSERT INTO Categories (Name, IsDeleted) VALUES (@Name, @IsDeleted) SELECT CAST(SCOPE_IDENTITY() AS int);";
	private const string DeleteCategorySql = @"DELETE FROM Categories WHERE Id = @Id";
	private const string GetAllCategoriesSql = @"SELECT * FROM Categories WHERE IsDeleted = 0";
	private const string GetCategoryIdByNameSql = @"SELECT Id FROM Categories WHERE Name = @Name AND IsDeleted = 0";
	private const string GetCategoryByIdSql = @"SELECT * FROM Categories WHERE Id = @Id";

	private readonly string _connectionString;

	public CategoryDAO(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<int> CreateCategoryAsync(Category category)
	{
		using var connection = new SqlConnection(_connectionString);

		try
		{
			var parameters = new
			{
				category.Name,
				category.IsDeleted
			};

			var newCategoryId = await connection.QuerySingleAsync<int>(InsertCategorySql, parameters);
			return newCategoryId;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error creating category: {ex.Message}", ex);
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

			var result = await connection.ExecuteAsync(DeleteCategorySql, parameters);
			return result > 0;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error deleting category: {ex.Message}", ex);
		}
	}
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        using var connection = new SqlConnection(_connectionString);

		try
		{
			var categories = await connection.QueryAsync<Category>(GetAllCategoriesSql);
			return categories;
		}
		catch (Exception ex)
		{

			throw new Exception($"Error retrieving categories: {ex.Message}", ex);
		}
    }

    public async Task<int?> GetCategoryIdByName(string categoryName)
    {
        using var connection = new SqlConnection(_connectionString);

		try
		{
			return await connection.QuerySingleOrDefaultAsync<int?>(GetCategoryIdByNameSql, new { Name = categoryName });
		}
		catch (Exception ex)
		{
			throw new Exception($"Error retrieving category {categoryName}: {ex.Message}", ex);
		}
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        using var connection = new SqlConnection(_connectionString);

		try
		{
			return await connection.QuerySingleOrDefaultAsync<Category>(GetCategoryByIdSql, new { Id = categoryId });
		}
		catch (Exception ex)
		{
			throw new Exception($"Error retrieving category with Id: {categoryId}: {ex.Message}", ex);
		}
    }
}
