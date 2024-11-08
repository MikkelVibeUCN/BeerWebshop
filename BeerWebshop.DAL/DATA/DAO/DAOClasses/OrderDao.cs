using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
    public class OrderDao : IOrderDAO
    {
        private readonly string _connectionString;
        private const string SQL_INSERT_ORDER = "INSERT INTO ORDERS(CreatedAt, IsDelivered, CustomerId_FK, IsDeleted) OUTPUT INSERTED.Id VALUES(GetDate(),@IsDelivered,@CustomerId_FK,@IsDeleted)";
        private const string SQL_INSERT_ORDERLINE = "INSERT INTO ORDERLINES(OrderId,ProductId, Quantity,Total) VALUES (@OrderId,@ProductId,@Quantity,@Total)";
        private const string SQL_UPDATE_STOCK = "UPDATE PRODUCTS SET Stock = Stock - @Quantity WHERE Id = @ProductId";

        public OrderDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> SaveOrderAsync(Order order)
        {
            using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();

            try
            {


                var orderId = await InsertOrderAsync(connection, transaction, order);

                foreach (var orderline in order.OrderLines)
                {
                    orderline.Id = orderId;
                    await InsertOrderLinesAsync(connection, transaction, orderId, orderline);
                    await UpdateStockAsync(connection, transaction, orderline);
                }
                await transaction.CommitAsync();

                return orderId;

            }
            catch (Exception ex)
            {
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Error saving order{ex.Message}");
                }
            }
        }

        private async Task<int> InsertOrderAsync(SqlConnection connection, IDbTransaction transaction, Order order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IsDelivered", order.IsDelivered);
            parameters.Add("@CustomerId_FK", order.CustomerId_FK);
            parameters.Add("IsDeleted", order.IsDeleted);

            try
            {

                return await connection.QuerySingleAsync<int>(SQL_INSERT_ORDER, parameters, transaction);

            }
            catch (Exception ex)
            {
                {
                    throw new Exception($"Could not insert order{ex.Message}");
                }
            }
        }

        public async Task InsertOrderLinesAsync(SqlConnection connection, IDbTransaction transaction, int orderId, OrderLine orderLine)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderId", orderId);
            parameters.Add("@ProductId", orderLine.ProductId);
            parameters.Add("@Quantity", orderLine.Quantity);
            parameters.Add("@Total", orderLine.SubTotal);

            await connection.ExecuteAsync(SQL_INSERT_ORDERLINE, parameters, transaction);

        }

        private async Task UpdateStockAsync(SqlConnection connection, IDbTransaction transaction, OrderLine orderLine)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Quantity", orderLine.Quantity);
            parameters.Add("@ProductId", orderLine.Product.Id);

            var rowsAffected = await connection.ExecuteAsync(SQL_UPDATE_STOCK, parameters, transaction);
            if (rowsAffected == 0)
            {
                throw new Exception($"No more left of this product");
            }

        }
    }
}
