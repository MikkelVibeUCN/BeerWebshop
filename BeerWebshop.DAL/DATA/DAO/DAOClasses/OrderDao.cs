using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
	public class OrderDAO : IOrderDAO
	{
		private readonly string _connectionString;

		private const string _insertOrderSql = @"
            INSERT INTO Orders (CreatedAt, IsDelivered, IsDeleted)
            OUTPUT INSERTED.Id 
            VALUES (@CreatedAt, @IsDelivered, @IsDeleted);";

		private const string _insertOrderLineSql = @"
            INSERT INTO OrderLines (OrderId, ProductId, Quantity, Total)
            VALUES (@OrderId, @ProductId, @Quantity, @Total);";

		private const string _getOrderByIdSql = @"
			SELECT o.Id AS Id, o.CreatedAt, o.IsDelivered, o.CustomerId_FK AS CustomerId, o.IsDeleted AS OrderIsDeleted,
			ol.OrderId, ol.ProductId, ol.Quantity, ol.Total AS OrderLineTotal,
			p.Id AS ProductId, p.Name AS ProductName, p.CategoryId_FK AS CategoryId, p.BreweryId_FK AS BreweryId,
			p.Price AS ProductPrice, p.Description AS ProductDescription, p.Stock AS ProductStock, p.Abv, 
			p.RowVersion, p.ImageUrl, p.IsDeleted AS ProductIsDeleted
			FROM Orders o
			LEFT JOIN Orderlines ol ON o.Id = ol.OrderId
			LEFT JOIN Products p ON ol.ProductId = p.Id
			WHERE o.Id = @Id;";


		private const string _deleteOrderByIdSql = @"
			DELETE FROM Orders WHERE Id = @Id";

		public OrderDAO(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<bool> DeleteOrderByIdAsync(int orderId)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();
			var rowsAffected = await connection.ExecuteAsync(_deleteOrderByIdSql, new { Id = orderId });
			return rowsAffected > 0;
		}


		public async Task<Order?> GetByIdAsync(int id)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			try
			{
				var parameters = new { Id = id };
				var order = await connection.QueryAsync<Order, Product, OrderLine>(_getOrderByIdSql, parameters);

				if (order == null)
				{
					return null;
				}

				return order;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error getting order from database: {ex.Message}", ex);
			}
		}


		public async Task<int> InsertCompleteOrderAsync(Order order)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();
			using var transaction = await connection.BeginTransactionAsync();

			try
			{
				var orderId = await InsertOrderAsync(connection, transaction, order);

				foreach (var orderLine in order.OrderLines)
				{
					await InsertOrderLineAsync(connection, transaction, orderLine, orderId);
				}

				await transaction.CommitAsync();

				return orderId;
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				throw new Exception($"Error inserting order: {ex.Message}");
			}
		}

		private async Task<int> InsertOrderAsync(SqlConnection connection, IDbTransaction transaction, Order order)
		{
			var parameters = new
			{
				CreatedAt = order.Date,
				IsDelivered = order.IsDelivered,
				IsDeleted = order.IsDeleted
			};

			return await connection.QuerySingleAsync<int>(_insertOrderSql, parameters, transaction);
		}

		private async Task InsertOrderLineAsync(SqlConnection connection, IDbTransaction transaction, OrderLine orderLine, int orderId)
		{
			var parameters = new
			{
				OrderId = orderId,
				ProductId = orderLine.Product.Id,
				Quantity = orderLine.Quantity,
				Total = orderLine.SubTotal
			};

			await connection.ExecuteAsync(_insertOrderLineSql, parameters, transaction);
		}
	}
}
