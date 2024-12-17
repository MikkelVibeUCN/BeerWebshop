using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class CategoryDAO : ICategoryDAO
{
    #region SQL Query
    private const string InsertCategorySql = @"INSERT INTO Categories (Name) VALUES (@Name) SELECT CAST(SCOPE_IDENTITY() AS int);";
    private const string DeleteCategorySql = @"DELETE FROM Categories WHERE Id = @Id";
    private const string GetByIdSql = @"SELECT * FROM Categories WHERE id = @Id";
    private const string GetCategoryIdByNameSql = @"SELECT Id FROM Categories WHERE Name = @Name";
    private const string GetAllCategoriesSql = @"SELECT * FROM Categories";
    #endregion

    #region Dependency injection
    private readonly string _connectionString;
    public CategoryDAO(string connectionString)
    {
        _connectionString = connectionString;
    }
    #endregion

    #region BaseDAO Methods
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
    public async Task<Category?> GetByIdAsync(int categoryId)
    {
        using var connection = new SqlConnection(_connectionString);

        try
        {
            await connection.OpenAsync();
            return await connection.QuerySingleOrDefaultAsync<Category>(GetByIdSql, new { Id = categoryId });
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the category.", ex);
        }
    }

    public Task<bool> UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
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
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            var categories = await connection.QueryAsync<Category>(GetAllCategoriesSql);
            return categories;
        }
        catch (Exception ex)
        {

            throw new Exception($"Error fetching categories: {ex.Message}", ex);
        }
    }
    #endregion

    #region ICategoryDAO Methods
    public async Task<int?> GetCategoryIdByName(string categoryName)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<int?>(GetCategoryIdByNameSql, new { Name = categoryName });
        }
        catch (Exception ex)
        {

            throw new Exception($"Error fetching category: {ex.Message}", ex);
        }
    }
}
#endregion
