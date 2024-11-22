using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Tools;
using System.Data.SqlClient;

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

        //new
        public async Task<int> CreateOrderFromDTOAsync(OrderDTO dto)
        {
            if (dto.OrderLines == null || !dto.OrderLines.Any())
            {
                throw new InvalidOperationException("Order must have at least one order line.");
            }

            var orderLines = new List<OrderLine>();

            foreach (var dtoOrderLine in dto.OrderLines)
            {
                if (dtoOrderLine.Product == null || dtoOrderLine.Product.Id == null)
                {
                    throw new InvalidOperationException("Order line must have a valid product.");
                }

                var product = await _productService.GetProductEntityByIdAsync((int)dtoOrderLine.Product.Id);
                if (product == null || product.IsDeleted || product.Stock < dtoOrderLine.Quantity)
                {
                    throw new InvalidOperationException("Invalid product details or insufficient stock.");
                }

                var orderLine = MappingHelper.MapOrderLineDtoToEntity(dtoOrderLine, product);
                orderLines.Add(orderLine);
            }

            var order = MappingHelper.MapOrderDTOToEntity(dto, orderLines);

            if (dto.CustomerDTO == null)
            {
                order.Customer = null;
            }

            return await CreateOrderAsync(order);
        }



        //new
        public async Task<int> CreateOrderAsync(Order order)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                foreach (var orderLine in order.OrderLines)
                {
                    if (orderLine.Product == null)
                    {
                        throw new InvalidOperationException("OrderLine product cannot be null.");
                    }

                    var success = await _productService.UpdateStockAsync((int)orderLine.Product.Id, orderLine.Quantity, connection, transaction);
                    if (!success)
                    {
                        throw new InvalidOperationException("Insufficient stock.");
                    }
                }

                var orderId = await _orderDao.CreateAsync(order, connection, transaction);
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



        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderDao.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} was not found.");
            }

            return order;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            var orders = await _orderDao.GetAllAsync();
            return orders.Select(MappingHelper.MapOrderEntityToDTO).ToList();
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _orderDao.GetOrdersByCustomerIdAsync(customerId);
            return orders.Select(MappingHelper.MapOrderEntityToDTO).ToList();
        }


        public async Task<bool> DeleteOrderByIdAsync(int orderId)
        {
            return await _orderDao.DeleteAsync(orderId);
        }


    }
}
