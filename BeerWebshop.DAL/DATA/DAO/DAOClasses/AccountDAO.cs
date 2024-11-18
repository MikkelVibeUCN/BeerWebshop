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
        c.Id AS Id, 
        CONCAT(c.FirstName, ' ', c.LastName) AS Name, 
        c.Phone AS Phone, 
        c.PasswordHash AS Password, 
        c.IsDeleted AS IsDeleted, 
        c.Age AS Age, 
        c.Email AS Email,
        CONCAT(
            a.Street, ' ', a.StreetNumber, 
            CASE WHEN a.ApartmentNumber IS NOT NULL THEN CONCAT(' ', a.ApartmentNumber) ELSE '' END, 
            ' ', p.Postalcode, ' ', p.City
        ) AS Address
        FROM 
            Customers c
        JOIN 
            Address a ON a.CustomerId_FK = c.Id
        JOIN 
            Postalcode p ON a.Postalcode_FK = p.Postalcode
        WHERE 
            c.Id = @Id;";


        private const string _saveCustomer = @"
            INSERT INTO Customers (FirstName, LastName, Phone, PasswordHash, Age, Email, IsDeleted)
            VALUES (@FirstName, @LastName, @Phone, @PasswordHash, @Age, @Email, 0);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        private const string _createAddress = @"
            INSERT INTO Address (Street, StreetNumber, ApartmentNumber, Postalcode_FK, CustomerId_FK)
            VALUES (@Street, @StreetNumber, @ApartmentNumber, @Postalcode, @CustomerId)
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        private const string _customerWithEmailExists = @"SELECT Id FROM Customers WHERE Email = @Email;";

        private const string _createZipCode = @"INSERT INTO Postalcode (PostalCode, City) VALUES (@ZipCode, @City);";

        private const string _deleteCustomerById = @"DELETE FROM Customers WHERE Id = @Id;";

        private const string _doesZipExist = @"SELECT PostalCode FROM Postalcode WHERE Postalcode = @ZipCode;";
        public AccountDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> DoesCustomerWithEmailExist(string email)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var idFound = await connection.QuerySingleOrDefaultAsync<int?>(_customerWithEmailExists, new { Email = email });
                return idFound != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int?> CreateAddress(Customer customer, SqlConnection connection, SqlTransaction transaction)
        {
            string[] parts = customer.Address.Split(' ');

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
                await CreateZip(zipCode, city, connection, transaction);
            }

            try
            {
                var addressParameters = new
                {
                    CustomerId = customer.Id,
                    Street = street,
                    StreetNumber = streetNumber,
                    ApartmentNumber = apartmentNumber,
                    Postalcode = zipCode
                };

                await connection.ExecuteAsync(_createAddress, addressParameters, transaction);

                return customer.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> CreateZip(int zipCode, string city, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                var parameters = new { ZipCode = zipCode, City = city };
                int rowsAffected = await connection.ExecuteAsync(_createZipCode, parameters, transaction);

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

        public async Task<Customer?> GetByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var parameters = new { Email = email };
                int? customerId = await connection.QuerySingleOrDefaultAsync<int?>(_customerWithEmailExists, parameters);

                await connection.CloseAsync();

                return await GetCustomerByIdAsync((int)customerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting customer from database: {ex.Message}", ex);
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
            if(customer.Email == null) { throw new Exception("Email cannot be null"); }
            if(await DoesCustomerWithEmailExist(customer.Email)) { throw new Exception("Customer with this email already exists"); }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();


            // Split the full name into first and last names
            var parts = customer.Name?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string firstName = parts[0];
            string lastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";

            var parameters = new
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = customer.Phone,
                PasswordHash = BCryptTool.HashPassword(customer.Password),
                Age = customer.Age,
                Email = customer.Email
            };

            try
            {
                int customerId = await connection.QuerySingleAsync<int>(_saveCustomer, parameters, transaction);

                customer.Id = customerId;

                await CreateAddress(customer, connection, transaction);

                await transaction.CommitAsync();

                return customerId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error saving customer", ex);
            }
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
