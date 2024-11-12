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
			SELECT o.Id, o.CreatedAt, o.IsDelivered, o.IsDeleted,
                       ol.Quantity, ol.Total,
                       p.Id, p.Name, p.Price, p.Description, p.Stock, p.Abv, p.ImageUrl,
                       c.Id, c.Name, c.IsDeleted,
                       b.Id, b.Name, b.IsDeleted
                FROM Orders o
                LEFT JOIN OrderLines ol ON o.Id = ol.OrderId
                LEFT JOIN Products p ON ol.ProductId = p.Id
                LEFT JOIN Categories c ON p.CategoryId_FK = c.Id
                LEFT JOIN Breweries b ON p.BreweryId_FK = b.Id
                WHERE o.Id = @Id";


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
                Order? orderResult = null;

                connection.Query<Order, OrderLine, Product, Category, Brewery, Order>(
                    _getOrderByIdSql,
                    (order, orderLine, product, category, brewery) =>
                    {
                        if (orderResult == null)
                        {
							orderResult = order;
                            orderResult.OrderLines = new List<OrderLine>();
                        }
						
						product.Brewery = brewery;
                        product.Category = category;

                        orderLine.Product = product;
                        orderResult.OrderLines.Add(orderLine);

                        return order;
                    },
                    new { Id = id },
                    splitOn: "Quantity,Id,Id,Id"
                );

                return orderResult;
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
				CreatedAt = order.CreatedAt,
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
