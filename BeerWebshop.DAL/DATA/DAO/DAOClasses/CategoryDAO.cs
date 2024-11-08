using BeerWebshop.DAL.DATA.Entities;
using System.Data.SqlClient;
using Dapper;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
	public class CategoryDAO
	{
		private const string _insertCategorySql = @"INSERT INTO Categories (Name, IsDeleted)
													VALUES (@Name, @IsDeleted);
													SELECT CAST(SCOPE_IDENTITY() AS int);";
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
	}
}
