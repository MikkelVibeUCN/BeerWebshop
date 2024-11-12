using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
    public class AccountDAO : IAccountDAO
    {
        private readonly string _connectionString;
        private const string _getCustomerById = @"SELECT * FROM Customers WHERE Id = @Id;";
        private const string _saveCustomer = @"
    DECLARE @AddressId INT;
    INSERT INTO Address (Street, StreetNumber, ApartmentNumber, Postalcode_FK)
    OUTPUT INSERTED.Id
    VALUES (@Street, @StreetNumber, @ApartmentNumber, @Postalcode);

    SET @AddressId = SCOPE_IDENTITY();

    INSERT INTO Customers (FirstName, LastName, Phone, PasswordHash, AddressId_FK, Age, Email, IsDeleted)
    OUTPUT INSERTED.Id
    VALUES (@FirstName, @LastName, @Phone, @PasswordHash, @AddressId, @Age, @Email, 0);
";

        private const string _deleteCustomerById = @" 
    DELETE FROM Address WHERE Id = (SELECT AddressId_FK FROM Customers WHERE Id = @Id);

    DELETE FROM Customers WHERE Id = @Id";
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

        public async Task<int> SaveCustomerAsync(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Deler navnet op i to variable som i databasen, og gemmer dem begge i name
            var nameParts = customer.Name?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            string firstName = nameParts.Length > 0 ? nameParts[0] : "";
            string lastName = nameParts.Length > 1
                ? string.Join(" ", nameParts.Skip(1))
                : "";

            var parameters = new
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = customer.Phone,
                PasswordHash = customer.Password, // Ensure this is a hashed password
                Street = customer.Address,
                StreetNumber = "69", // Add real value if available
                ApartmentNumber = "x", // Add real value if available
                Postalcode = customer.ZipCode, // Using postal code as a unique identifier
                Age = customer.Age,
                Email = customer.Email
            };
            return await connection.QuerySingleAsync<int>(_saveCustomer, parameters);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var parameters = new { Id = id };
                await connection.ExecuteAsync(_deleteCustomerById, parameters);
                return true;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error deleting the customer: {ex.Message}");
            }
        }
    }
}
