using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.Stubs
{
    public class OrderDAOStub : IOrderDAO
    {
        private readonly List<Order> _orders;
        private readonly List<Product> _products;

        public OrderDAOStub()
        {
            // Initialize sample products
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Golden Ale", Price = 3.99f },
                new Product { Id = 2, Name = "Hoppy IPA", Price = 4.99f },
                new Product { Id = 3, Name = "Dark Stout", Price = 5.49f },
                new Product { Id = 4, Name = "Summer Lager", Price = 2.99f }
            };

            // Initialize sample orders
            _orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    IsDelivered = false,
                    IsDeleted = false,
                    DeliveryAddress = "123 Brewery Lane",
                    Customer = new Customer { Id = 1, Name = "John Doe" },
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine(2, _products[0]), // 2 Golden Ales
                        new OrderLine(1, _products[1])  // 1 Hoppy IPA
                    }
                },
                new Order
                {
                    Id = 2,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    IsDelivered = true,
                    IsDeleted = false,
                    DeliveryAddress = "456 Ale Street",
                    Customer = new Customer { Id = 2, Name = "Jane Smith" },
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine(3, _products[2]), // 3 Dark Stouts
                        new OrderLine(2, _products[3])  // 2 Summer Lagers
                    }
                }
            };
        }

        // Create a new order
        public async Task<int> CreateAsync(Order order)
        {
            var nextId = _orders.Max(o => o.Id) + 1 ?? 1;
            order.Id = nextId;
            _orders.Add(order);
            return await Task.FromResult(order.Id.Value);
        }

        // Get order by ID
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_orders.SingleOrDefault(o => o.Id == id && !o.IsDeleted));
        }

        // Update an existing order
        public async Task<bool> UpdateAsync(Order order)
        {
            var existingOrder = _orders.SingleOrDefault(o => o.Id == order.Id && !o.IsDeleted);
            if (existingOrder == null)
                return await Task.FromResult(false);

            existingOrder.CreatedAt = order.CreatedAt;
            existingOrder.OrderLines = order.OrderLines;
            existingOrder.DeliveryAddress = order.DeliveryAddress;
            existingOrder.IsDelivered = order.IsDelivered;
            existingOrder.Customer = order.Customer;

            return await Task.FromResult(true);
        }

        // Delete an order by marking it as deleted
        public async Task<bool> DeleteAsync(int id)
        {
            var existingOrder = _orders.SingleOrDefault(o => o.Id == id && !o.IsDeleted);
            if (existingOrder == null)
                return await Task.FromResult(false);

            existingOrder.IsDeleted = true;
            return await Task.FromResult(true);
        }

        // Retrieve all orders (excluding deleted ones)
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await Task.FromResult(_orders.Where(o => !o.IsDeleted).ToList());
        }

        // Get orders by customer ID
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await Task.FromResult(_orders
                .Where(o => o.Customer.Id == customerId && !o.IsDeleted)
                .ToList());
        }

        public Task<bool> UpdateStockAsync(int productId, int quantity, SqlConnection? connection = null, DbTransaction? transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}