using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BeerWebshop.RESTAPI.Services
{
	public class OrderService
	{
		private readonly IOrderDAO _orderDao;
		private readonly ProductService _productService;
		private readonly string _connectionString;


		public OrderService(IOrderDAO orderDao, ProductService productService, string connectionString)
		{
			_orderDao = orderDao;
			_connectionString = connectionString;
			_productService = productService;
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
					var product = await _productService.GetProductByIdAsync(orderLine.ProductId);
					if (product == null || product.IsDeleted || product.Stock < orderLine.Quantity)
					{
						throw new InvalidOperationException("Invalid product details or insufficient stock.");
					}

					orderLine.Product = product;

					var success = await _productService.UpdateStockAsync(orderLine.ProductId, orderLine.Quantity, product.RowVersion);
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
			var order = await _orderDao.GetByIdAsync(orderId);
			if (order == null)
			{
				throw new KeyNotFoundException($"Order with ID {orderId} was not found.");
			}

			return order;
		}


		public async Task<bool> DeleteOrderByIdAsync(int orderId)
		{
			return await _orderDao.DeleteOrderByIdAsync(orderId);
		}
	}
}
