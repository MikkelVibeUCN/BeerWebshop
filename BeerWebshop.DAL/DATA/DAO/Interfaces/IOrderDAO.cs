using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.Interface;

public interface IOrderDAO
{
	Task<int> InsertCompleteOrderAsync(Order order);
	Task<Order?> GetByIdAsync(int id);
	Task<bool> DeleteOrderByIdAsync(int id);

}
