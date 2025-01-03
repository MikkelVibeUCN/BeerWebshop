﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services.Interfaces;
using BeerWebshop.RESTAPI.Tools;

namespace BeerWebshop.RESTAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAO _orderDao;
        private readonly IProductService _productService;
        private readonly string _connectionString;


        public OrderService(IOrderDAO orderDao, IProductService productService, string connectionString)
        {
            _orderDao = orderDao;
            _connectionString = connectionString;
            _productService = productService;
        }

        
        public async Task<int> CreateOrderFromDTOAsync(OrderDTO dto)
        {
            var orderLines = new List<OrderLine>();

            foreach (var dtoOrderLine in dto.OrderLines)
            {
                var product = await _productService.GetProductEntityByIdAsync((int)dtoOrderLine.Product.Id);
                if (product == null || product.Stock < dtoOrderLine.Quantity)
                {
                    throw new InvalidOperationException("Invalid product details or insufficient stock.");
                }

                var orderLine = MappingHelper.MapOrderLineDtoToEntity(dtoOrderLine, product);
                orderLines.Add(orderLine);
            }

            var order = MappingHelper.MapOrderDTOToEntity(dto, orderLines);

            return await CreateOrderAsync(order);
        }

        
        public async Task<int> CreateOrderAsync(Order order) => await _orderDao.CreateAsync(order);


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

        public async Task<bool> UpdateStockAsync(int productId, int quantity)
        {
            return await _orderDao.UpdateStockAsync(productId, quantity);

        }

    }
}
