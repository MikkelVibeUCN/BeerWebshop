using BeerWebshop.DAL.DATA.Entities;
using System.Data.Common;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IOrderDAO : IBaseDAO<Order>
{
	Task<int> CreateAsync(Order order, SqlConnection? connection = null, DbTransaction? transaction = null);
	Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
}
