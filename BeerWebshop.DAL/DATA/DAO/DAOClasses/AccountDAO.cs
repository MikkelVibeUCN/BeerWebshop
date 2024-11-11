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
    public class AccountDAO : IAccountDAO
    {
        private readonly string _connectionString;
        private const string _getCustomerById = @"SELECT * FROM Customers WHERE Id = @Id;";
        private const string _SaveCusomter = @"INSERT INTO Customers (FirstName, LastName, Email, PasswordHash, ";

        public AccountDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var parameters = new { Id = id };
                var customer = await connection.QuerySingleOrDefaultAsync<Customer>(_getCustomerById, parameters);

                return customer;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error getting customer from database: {ex.Message}", ex);
            }
        }

        public Task<int> SaveCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
