using BeerWebshop.DAL.BCrypt;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
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
            INSERT INTO Customers (FirstName, LastName, Phone, PasswordHash, AddressId_FK, Age, Email, IsDeleted)
            OUTPUT INSERTED.Id
            VALUES (@FirstName, @LastName, @Phone, @PasswordHash, @AddressId, @Age, @Email, 0);
            SELECT @CustomerId = SCOPE_IDENTITY();
        ";

        private const string _createAddress = @"INSERT INTO Address (Street, StreetNumber, ApartmentNumber, Postalcode_FK)
            VALUES (@Street, @StreetNumber, @ApartmentNumber, @Postalcode) OUTPUT INSERTED.Id";

        private const string _createZipCode = @"INSERT INTO Postalcode (Postalcode, City) VALUES (@ZipCode, @City);";

        private const string _deleteCustomerById = @" 
            DELETE FROM Address WHERE Id = (SELECT AddressId_FK FROM Customers WHERE Id = @Id);

            DELETE FROM Customers WHERE Id = @Id";

        private const string _doesZipExist = @"SELECT COUNT(*) FROM Postalcode WHERE Postalcode = @ZipCode;";
        public AccountDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int?> CreateAddress(Customer customer, SqlConnection connection)
        {
            // Split the string by spaces
            string[] parts = customer.Address.Split(' ');

            // Initialize variables
            string street = parts[0];
            string streetNumber = parts[1];
            string apartmentNumber = null;
            int zipCode;
            string city;

            if (parts.Length == 5)
            {
                apartmentNumber = parts[2];
                zipCode = int.Parse(parts[3]);
                city = parts[4];
            }
            else if (parts.Length == 4)
            {
                zipCode = int.Parse(parts[2]);
                city = parts[3];
            }
            else
            {
                throw new FormatException("Address format is incorrect.");
            }

            var parameters = new
            {
                Street = street,
                StreetNumber = streetNumber,
                ApartmentNumber = apartmentNumber,
                City = city,
                Postalcode = zipCode,
            };

            int? zipCodeId = await GetZip(zipCode);

            if (zipCodeId == null)
            {
                zipCodeId = await CreateZip(zipCode, city, connection);
            }

            try
            {
                //var parameters = new { Street = street, StreetNumber = streetNumber, ApartmentNumber = apartmentNumber, Postalcode = zipCodeId };
                //int? id = 
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<int?> CreateZip(int zipCode, string city, SqlConnection connection)
        {
            try
            {
                var parameters = new { ZipCode = zipCode, City = city };
                int? id = await connection.QuerySingleOrDefaultAsync<int?>(_createZipCode, parameters);

                return id;
            }
            catch (Exception)
            {

                throw;
            }   
        }

        public async Task<int?> GetZip(int zipCode)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var parameters = new { ZipCode = zipCode };
                int? id = await connection.QuerySingleOrDefaultAsync<int?>(_doesZipExist, parameters);

                return id;
            }
            catch (Exception)
            {
                throw new Exception("Error getting zip");
            }
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

            // Split name into first and last names as required by the database
            var nameParts = customer.Name?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            string firstName = nameParts.Length > 0 ? nameParts[0] : "";
            string lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : "";

            var parameters = new
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = customer.Phone,
                PasswordHash = customer.Password,
                Street = customer.Address,
                StreetNumber = "",
                ApartmentNumber = "",
                Postalcode = int.Parse(customer.ZipCode),
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

        public Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<int> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
