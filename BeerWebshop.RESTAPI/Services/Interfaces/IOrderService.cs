using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderFromDTOAsync(OrderDTO dto);
        Task<int> CreateOrderAsync(Order order);
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(int customerId);
        Task<bool> DeleteOrderByIdAsync(int orderId);
        Task<bool> UpdateStockAsync(int productId, int quantity);



    }
}
