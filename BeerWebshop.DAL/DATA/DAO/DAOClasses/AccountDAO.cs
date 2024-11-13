using BeerWebshop.DAL.BCrypt;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
    public class AccountDAO : IAccountDAO
    {
        private readonly string _connectionString;

        private const string _getCustomerById = @"SELECT 
                c.id AS Id, 
                CONCAT(c.firstName, ' ', c.lastName) AS Name, 
                c.phone as Phone, 
                c.passwordHash AS Password, 
                c.isDeleted AS IsDeleted, 
                c.age AS Age, 
                c.email AS Email,
                -- Combine address components into a single string in SQL
                CONCAT(a.street, ' ', a.streetNumber, 
                       CASE WHEN a.apartmentNumber IS NOT NULL THEN CONCAT(' ', a.apartmentNumber) ELSE '' END, 
                       ', ', p.city, ' ', p.postalCode) AS Address
                FROM 
                    Customers c
                JOIN 
                    Address a ON c.addressId_FK = a.id
                JOIN 
                    PostalCode p ON a.postalCode_FK = p.postalCode
                WHERE 
                    c.id = @Id";
        private const string _saveCustomer = @"
            INSERT INTO Customers (FirstName, LastName, Phone, PasswordHash, AddressId_FK, Age, Email, IsDeleted)
            VALUES (@FirstName, @LastName, @Phone, @PasswordHash, @AddressId, @Age, @Email, 0) SELECT CAST(SCOPE_IDENTITY() AS int);";

        private const string _createAddress = @"INSERT INTO Address (Street, StreetNumber, ApartmentNumber, Postalcode_FK)
            VALUES (@Street, @StreetNumber, @ApartmentNumber, @Postalcode)
            SELECT CAST(SCOPE_IDENTITY() AS int)";

        private const string _createZipCode = @"INSERT INTO Postalcode (PostalCode, City) VALUES (@ZipCode, @City);";

        private const string _deleteCustomerById = @" 
            DELETE FROM Address WHERE Id = (SELECT AddressId_FK FROM Customers WHERE Id = @Id);

            DELETE FROM Customers WHERE Id = @Id";

        private const string _doesZipExist = @"SELECT PostalCode FROM Postalcode WHERE Postalcode = @ZipCode;";
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
            string? apartmentNumber = null;
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

            if (!await GetZipExistsAsync(zipCode))
            {
                await CreateZip(zipCode, city, connection);
            }
            try
            {
                var addressParameters = new { Street = street, StreetNumber = streetNumber, ApartmentNumber = apartmentNumber, Postalcode = zipCode };

                var id = await connection.QuerySingleAsync<int>(_createAddress, addressParameters);

                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateZip(int zipCode, string city, SqlConnection connection)
        {
            try
            {
                var parameters = new { ZipCode = zipCode, City = city };
                int rowsAffected = await connection.ExecuteAsync(_createZipCode, parameters);

                return rowsAffected > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> GetZipExistsAsync(int zipCode)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var parameters = new { ZipCode = zipCode };
                // Assuming _doesZipExist is a query that checks for existence of the zip code
                var result = await connection.QuerySingleOrDefaultAsync<int?>(_doesZipExist, parameters);

                // Return true if the result is not null (meaning the row exists)
                return result.HasValue;
            }
            catch (Exception)
            {
                throw new Exception("Error checking if zip exists.");
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var parameters = new { Id = id };
                return await connection.QuerySingleOrDefaultAsync<Customer>(_getCustomerById, parameters);
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

            int? addressId = await CreateAddress(customer, connection);

            if (addressId == null)
            {
                throw new Exception("Error creating address");
            }
            var parts = customer.Name?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string firstName = parts[0];
            string lastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";

            var parameters = new
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = customer.Phone,
                PasswordHash = BCryptTool.HashPassword(customer.Password),
                AddressId = addressId,
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
