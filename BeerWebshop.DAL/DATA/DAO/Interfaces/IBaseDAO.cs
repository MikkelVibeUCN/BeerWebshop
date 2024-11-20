using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces
{
    public interface IBaseDAO<T>
    {
        Task<int> CreateAsync (T entity);
        Task<T?> GetByIdAsync(int id);
        Task<bool> UpdateAsync (T entity);
        Task<bool>DeleteAsync (int id);
        Task<IEnumerable<T>> GetAllAsync ();
    }
}
