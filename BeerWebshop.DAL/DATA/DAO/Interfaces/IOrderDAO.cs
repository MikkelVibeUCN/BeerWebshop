using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IOrderDAO
{
	Task<int> InsertCompleteOrderAsync(Order order);
	Task<Order?> GetByIdAsync(int id);
	Task<bool> DeleteOrderByIdAsync(int id);

}
