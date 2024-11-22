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
            return await _accountDAO.GetByEmail(email);
        }
        public async Task DeleteCustomer(int id)
        {
            await _accountDAO.DeleteAsync(id);
        }

        public async Task<string?> AuthenticateAndGetTokenAsync(LoginViewModel loginViewModel)
        {
            var customerDTO = await _accountDAO.GetByEmail(loginViewModel.Email);
            if (customerDTO != null && BCrypt.Net.BCrypt.Verify(loginViewModel.Password, customerDTO.Password))
            {
                return _jwtService.GenerateJwtToken(customerDTO.Email);
            }
            return null;
        }

        
    }
}