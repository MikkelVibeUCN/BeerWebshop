using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IOrderDAO : IBaseDAO<Order>
{
	Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
}
