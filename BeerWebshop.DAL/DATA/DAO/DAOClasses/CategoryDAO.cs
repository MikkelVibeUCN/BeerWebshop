using BeerWebshop.DAL.DATA.Entities;
using System.Data.SqlClient;
using Dapper;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class CategoryDAO : ICategoryDAO
{
	private const string _insertCategorySql = @"INSERT INTO Categories (Name, IsDeleted)
													VALUES (@Name, @IsDeleted);
													SELECT CAST(SCOPE_IDENTITY() AS int);";

	private const string _softDeleteCategorySql = @"UPDATE Categories
												SET IsDeleted = 1
												WHERE Id = @Id";

	private const string _deleteCategorySql = @"DELETE FROM Categories
												WHERE Id = @Id";

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
			var newCategoryId = await connection.QuerySingleAsync<int>(_insertCategorySql, parameters);
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

			var result = await connection.ExecuteAsync(_deleteCategorySql, parameters);
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

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
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
