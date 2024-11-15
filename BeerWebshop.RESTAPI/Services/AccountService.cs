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
            return await _accountDAO.SaveCustomerAsync(customer);
        }

        public async Task<CustomerDTO> GetCustomerFromEmail(string email)
        {
            // få fat i customer entity'
            var customer = await _accountDAO.GetCustomerByEmail(email);
            //Map entity til dto
            //returner dto
            return MappingHelper.MapToDTO(customer);
            

        }
    }
}
