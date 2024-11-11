using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;

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
    }
}
