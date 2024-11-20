using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Tools;

namespace BeerWebshop.RESTAPI.Services
{
    public class AccountService
    {
        private readonly IAccountDAO _accountDAO;
        public AccountService(IAccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public async Task<int> SaveCustomerAsync(Customer customer)
        {
            return await _accountDAO.CreateAsync(customer);
        }

        public async Task<CustomerDTO?> GetByEmail(string email)
        {
            var customer = await _accountDAO.GetByEmail(email);
            if(customer == null) { return null; }

            return MappingHelper.MapToDTO(customer);
        }
        public async Task DeleteCustomer(int id)
        {
            await _accountDAO.DeleteAsync(id);
        }
    }
}