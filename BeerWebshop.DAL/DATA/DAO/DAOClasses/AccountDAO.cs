using BeerWebshop.DAL.BCrypt;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using Dapper;
using System.Data.Common;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
    public class AccountDAO : IAccountDAO
    {
        #region Sql query
        private const string _getCustomerByX = @"SELECT 
            c.AccountId AS Id, 
            CONCAT(c.FirstName, ' ', c.LastName) AS Name, 
            c.Phone AS Phone, 
            a.PasswordHash AS PasswordHash, 
            c.IsDeleted AS IsDeleted, 
            c.Age AS Age, 
            a.Email AS Email, 
            a.Role AS Role, 
            CONCAT(
                ad.Street, ' ', ad.StreetNumber, 
                CASE WHEN ad.ApartmentNumber IS NOT NULL THEN CONCAT(' ', ad.ApartmentNumber) ELSE '' END, 
                ' ', p.Postalcode, ' ', p.City
            ) AS Address
        FROM 
            Customers c
        JOIN 
            Address ad ON ad.CustomerId_FK = c.AccountId
        JOIN 
            Postalcode p ON ad.Postalcode_FK = p.Postalcode
        JOIN 
            Accounts a ON a.Id = c.AccountId
        WHERE
            ";


        private const string _createAccount = @"INSERT INTO Accounts (Role, Email, PasswordHash)
            VALUES (@Role, @Email, @PasswordHash) 
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        private const string _saveCustomer = @"
            INSERT INTO Customers (AccountId, FirstName, LastName, Phone, Age, IsDeleted)
            VALUES (@AccountId, @FirstName, @LastName, @Phone, @Age, 0);";

        private const string _createAddress = @"
            INSERT INTO Address (Street, StreetNumber, ApartmentNumber, Postalcode_FK, CustomerId_FK)
            VALUES (@Street, @StreetNumber, @ApartmentNumber, @Postalcode, @CustomerId)
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        private const string _accountWithEmailExists = @"SELECT Id FROM Accounts WHERE Email = @Email;";

        private const string _createZipCode = @"INSERT INTO Postalcode (PostalCode, City) VALUES (@ZipCode, @City);";

        private const string _deleteCustomerById = @"DELETE FROM Customers WHERE AccountId = @Id;";

        private const string _doesZipExist = @"SELECT PostalCode FROM Postalcode WHERE Postalcode = @ZipCode;";
        #endregion

        #region Dependency injection
        private readonly string _connectionString;
        public AccountDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region BaseDAO methods
        public async Task<int> CreateAsync(Customer customer)
        {
            if(customer.Email == null) { throw new Exception("Email cannot be null"); }
            if(await DoesAccountWithEmailExist(customer.Email)) { throw new Exception("Customer with this email already exists"); }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            var PasswordHash = customer.PasswordHash != null ? BCryptTool.HashPassword(customer.PasswordHash) : null;

            // Split the full name into first and last names
            var parts = customer.Name?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string firstName = parts[0];
            string lastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";
            
            try
            {
                int? accountId = await CreateAccount("User", PasswordHash, customer.Email, connection, transaction);

                customer.Id = accountId ?? throw new Exception("Account wasn't created");

                var parameters = new
                {
                    AccountId = accountId,
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = customer.Phone,
                    Age = customer.Age
                };


                await connection.QueryAsync(_saveCustomer, parameters, transaction);

                await CreateAddress(customer, connection, transaction);

                await transaction.CommitAsync();

                return customer.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Error saving customer", ex);
            }
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            try
            {
                var updatedQuery = _getCustomerByX + "c.Id = @Id;";

                var parameters = new { Id = id };
                var customer =  await connection.QuerySingleOrDefaultAsync<Customer>(updatedQuery, parameters);
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting customer from database: {ex.Message}", ex);
            }
        }

        public Task<bool> UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteAsync(int id)
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

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IAccountDAO methods
        public async Task<Account?> GetAccountByEmail(string email)
        {
            var accountType = await GetAccountTypeByEmail(email);

            return accountType switch
            {
                "User" => await GetCustomerByEmail(email),
                "Admin" => await GetAdminByEmail(email),
                _ => null
            };
        }

        private async Task<string?> GetAccountTypeByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"SELECT Role FROM Accounts WHERE Email = @Email;";
            return await connection.QuerySingleOrDefaultAsync<string>(query, new { Email = email });
        }

        private async Task<Customer?> GetCustomerByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = _getCustomerByX + "a.Email = @Email;";

            return await connection.QuerySingleOrDefaultAsync<Customer?>(query, new { Email = email });
        }
        
        private async Task<Admin?> GetAdminByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    ad.PermissionLevel
                    ac.Email, 
                    ac.Role, 
                    ac.PasswordHash,
                    ac.Id
                FROM Accounts ac 
                LEFT JOIN Admins ad ON ac.Id = ad.AccountId
                WHERE ad.Email = @Email;";

            return await connection.QuerySingleOrDefaultAsync<Admin>(query, new { Email = email });
        }

        public Task<int> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Helper methods for creating customer and validations
        public async Task<bool> DoesAccountWithEmailExist(string email)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var idFound = await connection.QuerySingleOrDefaultAsync<int?>(_accountWithEmailExists, new { Email = email });
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

        public async Task<int?> CreateAccount(string role, string passwordHash, string email, SqlConnection connection, DbTransaction transaction)
        {
            try
            {
                var parameters = new
                {
                    Role = role,
                    PasswordHash = passwordHash,
                    Email = email
                };
                return await connection.QuerySingleOrDefaultAsync<int>(_createAccount, parameters, transaction);
            }
            catch
            {
                throw new Exception("Failed to create account");
            }
        }
    }


}
#endregion


