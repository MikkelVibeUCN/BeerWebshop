using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Tools;

namespace BeerWebshop.RESTAPI.Services
{
    public class AccountService
    {

        private readonly JWTService _jwtService;
        private readonly IAccountDAO _accountDAO;
        public AccountService(IAccountDAO accountDAO, JWTService jwtService)
        {
            _accountDAO = accountDAO;
            _jwtService = jwtService;
        }

        public async Task<int> SaveCustomerAsync(Customer customer)
        {
            return await _accountDAO.CreateAsync(customer);
        }

        public async Task<Customer?> GetByEmail(string email)
        {
            var account = await _accountDAO.GetAccountByEmail(email);
            if (account is Customer customer)
            {
                return customer;
            }
            else return null;
        }
        public async Task DeleteCustomer(int id)
        {
            await _accountDAO.DeleteAsync(id);
        }

        public async Task<string?> AuthenticateAndGetTokenAsync(LoginViewModel loginViewModel)
        {
            var account = await _accountDAO.GetAccountByEmail(loginViewModel.Email);
            if (account != null && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, account.PasswordHash))
            {
                return _jwtService.GenerateJwtToken(account.Email, account.Role);
            }
            return null;
        }

        
    }
}