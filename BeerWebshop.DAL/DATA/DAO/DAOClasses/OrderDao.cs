using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
    public class OrderDao : IOrderDAO
    {
        private readonly string _connectionString;
        private const string SQL_INSERT_ORDER = "INSERT INTO ORDERS(Date, TotalPrice, DeliveryAddress, IsDelivered) OUTPUT INSERTED.Id VALUES(@Date,@DeliveryAddress,@IsDelivered)";
        private const string SQL_INSERT_ORDERLINE = "INSERT INTO ORDERLINES(Quantity,ProductId, OrderId) VALUES (@Quantity, @ProductId,@OrderId)";
        private const string SQL_UPDATE_STOCK = "UPDATE PRODUCTS SET Stock = Stock - @Quantity WHERE Id = @ProductId";

        public OrderDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> SaveOrderAsync(Order order)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var orderId = await connection.ExecuteScalarAsync<int>(SQL_INSERT_ORDER, order);
                foreach (var orderline in order.OrderLines)
                {
                    await connection.ExecuteAsync(SQL_INSERT_ORDERLINE, orderline);
                }

                await connection.ExecuteAsync(SQL_UPDATE_STOCK, order);


            return orderId;

            }
            catch(Exception ex) {
            {
                    throw new Exception($"Error saving order{ex.Message}");
            }
        }
    }
}
