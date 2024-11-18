using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces
{
    public interface IAccountDAO
    {
        Task<int> SaveCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(int id);

        Task<bool> DeleteCustomerAsync(int id);

        //methods relating to handling security

        Task<int> LoginAsync(string email, string password);
        Task<Customer?> GetByEmail(string email);

    }
}
