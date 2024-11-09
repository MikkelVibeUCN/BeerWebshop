using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BeerWebshop.RESTAPI.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderDAO _orderDao;
		private readonly IProductDAO _productDao;
		private readonly string _connectionString;

		public OrderService(IOrderDAO orderDao, IProductDAO productDao, string connectionString)
		{
			_orderDao = orderDao;
			_productDao = productDao;
			_connectionString = connectionString;
		}

		public async Task<int> CreateOrderAsync(Order order)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();
			using var transaction = await connection.BeginTransactionAsync();

			try
			{
				foreach (var orderLine in order.OrderLines)
				{
					var product = await _productDao.GetByIdAsync(orderLine.ProductId);
					if (product == null || product.IsDeleted || product.Stock < orderLine.Quantity)
					{
						throw new InvalidOperationException("Invalid product details or insufficient stock.");
					}

					orderLine.Product = product;

					var success = await _productDao.UpdateStockOptimisticAsync(orderLine.ProductId, orderLine.Quantity, product.RowVersion);
					if (!success)
					{
						throw new InvalidOperationException("The product stock was modified by another transaction.");
					}
				}

				var orderId = await _orderDao.InsertCompleteOrderAsync(order);

				await transaction.CommitAsync();

				return orderId;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}

		public async Task<Order?> GetOrderByIdAsync(int orderId)
		{
			return await _orderDao.GetByIdAsync(orderId);
		}
	}
}
