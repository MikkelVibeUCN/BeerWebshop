using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses
{
    public interface IAccountDAO
    {
        Task<int> SaveCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(int id);

        Task<bool> DeleteCustomerAsync(int id);

        //methods relating to handling security
        Task<bool> UpdatePasswordAsync(string email, string oldPassword, string newPassword);
        Task<int> LoginAsync(string email, string password);

    }
}
