using BeerWebshop.DAL.DATA.Entities;
using System.Data.Common;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IOrderDAO : IBaseDAO<Order>
{
	Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
	Task<bool> UpdateStockAsync(int productId, int quantity, SqlConnection? connection = null, DbTransaction? transaction = null);

}
