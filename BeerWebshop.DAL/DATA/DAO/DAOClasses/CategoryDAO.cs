using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class CategoryDAO : ICategoryDAO
{
	private const string InsertCategorySql = @"INSERT INTO Categories (Name, IsDeleted) VALUES (@Name, @IsDeleted) SELECT CAST(SCOPE_IDENTITY() AS int);";
	private const string DeleteCategorySql = @"DELETE FROM Categories WHERE Id = @Id";

	private readonly string _connectionString;

	public CategoryDAO(string connectionString)
	{
		_connectionString = connectionString;
	}

	public async Task<int> CreateAsync(Category category)
	{
		if (string.IsNullOrWhiteSpace(category.Name))
		{
			throw new ArgumentException("Category name cant be null or empty.");
		}

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
		catch (SqlException sqlEx) when (sqlEx.Number == 2627 || sqlEx.Number == 2601) //Sql unique constraint violation
		{
			throw new InvalidOperationException($"Category with name '{category.Name}' already exists.", sqlEx);
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
		var categories = await connection.QueryAsync<Category>("SELECT * FROM Categories WHERE IsDeleted = 0");
		return categories;
	}

	public async Task<int?> GetCategoryIdByName(string categoryName)
	{
		const string sql = "SELECT Id FROM Categories WHERE Name = @Name AND IsDeleted = 0";
		using var connection = new SqlConnection(_connectionString);
		return await connection.QuerySingleOrDefaultAsync<int?>(sql, new { Name = categoryName });
	}

	public async Task<Category?> GetByIdAsync(int categoryId)
	{
		const string sql = "SELECT * FROM Categories WHERE Id = @Id;";
		using var connection = new SqlConnection(_connectionString);
		return await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = categoryId });
	}

	public async Task<Category?> GetCategoryById(int id)
	{
		const string sql = "SELECT * FROM Categories WHERE Id = @Id;";
		using var connection = new SqlConnection(_connectionString);

		try
		{
			await connection.OpenAsync();
			return await connection.QuerySingleOrDefaultAsync<Category>(sql, new { Id = id });
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while retrieving the category.", ex);
		}
	}


}
