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
        private List<Order> _orders = new List<Order>();
        private int _nextId = 1;
        

        // Create a new order
        public  Task<int> CreateAsync(Order order)
        {
            order.Id = _nextId++;
            _orders.Add(order);
            return  Task.FromResult(order.Id.Value);
        }

        

        // Get order by ID
        public Task<Order?> GetByIdAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return Task.FromResult(order);
        }

        // Update an existing order
        public Task<bool> UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }

        // Delete an order by marking it as deleted
        public Task<bool> DeleteAsync(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if(order == null)
            {
                return Task.FromResult(false);
            }

            _orders.Remove(order);
            return Task.FromResult(true);
               
        }

        // Retrieve all orders (excluding deleted ones)
        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }

        // Get orders by customer ID
        public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {

            return  Task.FromResult(_orders
                .Where(o => o.Customer.Id == customerId && !o.IsDeleted)
                .AsEnumerable());
        }

        public Task<bool> UpdateStockAsync(int productId, int quantity, SqlConnection? connection = null, DbTransaction? transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}