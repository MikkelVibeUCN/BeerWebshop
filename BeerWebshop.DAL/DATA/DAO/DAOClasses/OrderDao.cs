using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
    public class OrderDAO : IOrderDAO
    {

        private const string InsertOrderSql = @"INSERT INTO Orders (CreatedAt, IsDelivered, IsDeleted, AddressId) OUTPUT INSERTED.Id VALUES (@CreatedAt, @IsDelivered, @IsDeleted, @AddressId);";
        private const string InsertOrderLineSql = @"INSERT INTO OrderLines (OrderId, ProductId, Quantity, Total) VALUES (@OrderId, @ProductId, @Quantity, @Total);";
        private const string DeleteOrderByIdSql = @"DELETE FROM Orders WHERE Id = @Id";
        private const string UpdateStockFromOrderSql = @"UPDATE PRODUCTS SET Stock = Stock - @Quantity WHERE Id = @ProductId";
        private const string BaseOrderSql = @"
            SELECT 
	            o.Id, 
	            o.CreatedAt, 
	            o.IsDelivered, 
	            o.IsDeleted, 
	            ol.Quantity , 
	            ol.Total, 
	            p.Id, 
	            p.Name, 
	            p.Price, 
	            p.Description, 
	            p.Stock, 
	            p.Abv, 
	            p.ImageUrl, 
	            p.RowVersion, 
	            c.Id, 
	            c.Name, 
	            c.IsDeleted, 
	            b.Id, 
	            b.Name, 
	            b.IsDeleted, 
	            cu.AccountId, 
	            CONCAT(cu.FirstName, ' ', cu.LastName) AS Name, 
	            cu.Phone, 
	            ac.PasswordHash, 
	            cu.IsDeleted, 
	            cu.Age, 
	            ac.Email,
	            CONCAT(
		            a.Street, ' ', a.StreetNumber, 
		            CASE WHEN a.ApartmentNumber IS NOT NULL THEN CONCAT(' ', a.ApartmentNumber) ELSE '' END, 
		            ' ', po.Postalcode, ' ', po.City
	            ) AS CustomerAddress
            FROM Orders o
            LEFT JOIN OrderLines ol ON o.Id = ol.OrderId
            LEFT JOIN Products p ON ol.ProductId = p.Id
            LEFT JOIN Categories c ON p.CategoryId_FK = c.Id
            LEFT JOIN Breweries b ON p.BreweryId_FK = b.Id
            LEFT JOIN Address a ON o.AddressId = a.Id
            LEFT JOIN Customers cu ON a.AccountId = cu.AccountId
            LEFT JOIN Accounts ac ON cu.AccountId = ac.Id
            LEFT JOIN Postalcode po ON a.Postalcode_FK = po.Postalcode
            ";

        private readonly string _connectionString;
        public OrderDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task UpdateStockFromOrder(IEnumerable<OrderLine> orderLines, SqlConnection connection, DbTransaction transaction)
        {
            foreach (var orderLine in orderLines)
            {
                var success = await UpdateStockAsync((int)orderLine.Product.Id, orderLine.Quantity, connection, transaction);
                if (!success)
                {
                    throw new InvalidOperationException("Insufficient stock.");
                }
            }
        }

        public async Task<int> CreateAsync(Order order)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync(IsolationLevel.RepeatableRead);
            Thread.Sleep(5000);

            try
            {
                //TODO: Rename method for clarification
                await UpdateStockFromOrder(order.OrderLines, connection, transaction);

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
                Console.WriteLine($"Error in CreateOrderAsync: {ex.Message}");
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                Order? orderResult = null;
                var sqlQuery = $"{BaseOrderSql} WHERE o.id = @Id";

                var result = await connection.QueryAsync<Order, OrderLine, Product, Category, Brewery, Customer, Order>(
                    sqlQuery,
                    (order, orderLine, product, category, brewery, customer) =>
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

                        orderResult.Customer = customer;

                        return order;
                    },
                    new { Id = id },
                    splitOn: "Quantity,Id,Id,Id,AccountId"
                );

                return orderResult;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting order from database: {ex.Message}", ex);
            }
        }
        public Task<bool> UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteAsync(int orderId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var rowsAffected = await connection.ExecuteAsync(DeleteOrderByIdSql, new { Id = orderId });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting order with ID: {orderId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateStockAsync(int productId, int quantity, SqlConnection? connection = null, DbTransaction? transaction = null)
        {
            bool needsToCpommit = false;
            if (connection == null)
            {
                connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
            }
            if(transaction == null)
            {
                needsToCpommit = true;
                transaction = await connection.BeginTransactionAsync();
            }

            var parameters = new DynamicParameters();
            parameters.Add("@ProductId", productId);
            parameters.Add("@Quantity", quantity);

            var stock = await connection.QuerySingleAsync<int>("SELECT Stock FROM Products WHERE Id = @ProductId", new { ProductId = productId }, transaction, commandTimeout: 5);
            //TODO: Shared lock on stock
            if (stock < quantity)
            {
                return false;
            }

            var rowsAffected = await connection.ExecuteAsync(UpdateStockFromOrderSql, parameters, transaction, commandTimeout: 5);
            if (rowsAffected < 0)
            {
                return false;
            }

            if (needsToCpommit)
            {
                await transaction.CommitAsync();
            }
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var orders = new List<Order>();

                var result = await connection.QueryAsync<Order, OrderLine, Product, Category, Brewery, Customer, Order>(
                    BaseOrderSql,
                    (order, orderLine, product, category, brewery, customer) =>
                    {
                        var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
                        if (existingOrder == null)
                        {
                            existingOrder = order;
                            existingOrder.OrderLines = new List<OrderLine>();
                            existingOrder.Customer = customer;
                            orders.Add(existingOrder);
                        }

                        if (product != null)
                        {
                            product.Brewery = brewery;
                            product.Category = category;
                        }

                        if (orderLine != null)
                        {
                            orderLine.Product = product;
                            existingOrder.OrderLines.Add(orderLine);
                        }

                        return existingOrder;
                    },
                    splitOn: "Quantity,Id,Id,Id, AccountId"
                );

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting orders from database: {ex.Message}", ex);
            }
        }
        #region IOrderDAO Methods

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var orders = new List<Order>();
                var sqlQuery = $"{BaseOrderSql} WHERE a.AccountId = @AccountId";

                var result = await connection.QueryAsync<Order, OrderLine, Product, Category, Brewery, Customer, Order>(
                    sqlQuery,
                    (order, orderLine, product, category, brewery, customer) =>
                    {
                        var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
                        if (existingOrder == null)
                        {
                            existingOrder = order;
                            existingOrder.OrderLines = new List<OrderLine>();
                            existingOrder.Customer = customer;
                            orders.Add(existingOrder);
                        }

                        if (product != null)
                        {
                            product.Brewery = brewery;
                            product.Category = category;
                        }

                        if (orderLine != null)
                        {
                            orderLine.Product = product;
                            existingOrder.OrderLines.Add(orderLine);
                        }

                        return existingOrder;
                    },
                    new { AccountId = customerId },
                    splitOn: "Quantity,Id,Id,Id,AccountId"
                );

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting orders for customerId {customerId} from database: {ex.Message}", ex);
            }
        }
        #endregion
        #region Create Order helper methods
        private async Task<int> InsertOrderAsync(SqlConnection connection, IDbTransaction transaction, Order order)
        {
            var parameters = new
            {
                CreatedAt = order.CreatedAt,
                IsDelivered = order.IsDelivered,
                IsDeleted = order.IsDeleted,
                AddressId = await GetAddressIdFromAccountId(order.Customer.Id)
            };

            return await connection.QuerySingleAsync<int>(InsertOrderSql, parameters, transaction);
        }

        private async Task<int> GetAddressIdFromAccountId(int accountId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var addressId = await connection.QuerySingleAsync<int>("SELECT Id FROM Address WHERE AccountId = @AccountId", new { AccountId = accountId });
            return addressId;

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

            await connection.ExecuteAsync(InsertOrderLineSql, parameters, transaction);
        }

        public Task<int> CreateAsync(Order order, SqlConnection? connection = null, DbTransaction? transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}
#endregion