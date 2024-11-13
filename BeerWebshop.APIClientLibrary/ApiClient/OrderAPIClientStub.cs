//using BeerWebshop.APIClientLibrary.ApiClient.Client;
//using BeerWebshop.APIClientLibrary.ApiClient.DTO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BeerWebshop.APIClientLibrary.ApiClient
//{
//    public class OrderAPIClientStub : IOrderApiClient
//    {
//        private static List<OrderDTO> _orders { get; set; } = new List<OrderDTO>();

//        public Task<OrderDTO?> GetOrderFromId(int id)
//        {
//            OrderDTO? order = _orders.FirstOrDefault(o => o.Id == id);
//            return Task.FromResult(order);
//        }

//        public Task<int> SaveOrder(OrderDTO order)
//        {
//            order.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;

//            _orders.Add(order);
//            return Task.FromResult(order.Id);
//        }
//    }
//}
