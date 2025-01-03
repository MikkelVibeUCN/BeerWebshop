﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.DAO.Stubs;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Tools;

namespace BeerWebshop.RESTAPI.Stubs
{
    public class OrderServiceStub
    {

        private ProductServiceStub _productServiceStub;
        private IOrderDAO _orderDaoStub;

        public OrderServiceStub()
        {
            _orderDaoStub = new OrderDAOStub();
            _productServiceStub = new ProductServiceStub();
        }



        public async Task<int> CreateOrderFromDtoStub(OrderDTO dto)
        {
            var orderLines = new List<OrderLine>();

            foreach (var dtoOrderLine in dto.OrderLines)
            {
                var product = await _productServiceStub.GetProductEntityByIdAsync((int)dtoOrderLine.Product.Id);
                if (product == null || product.Stock < dtoOrderLine.Quantity)
                {
                    throw new InvalidOperationException("Invalid product details or insufficient stock.");
                }

                var orderLine = MappingHelper.MapOrderLineDtoToEntity(dtoOrderLine, product);
                orderLines.Add(orderLine);
            }

            var order = MappingHelper.MapOrderDTOToEntity(dto, orderLines);

            return await CreateOrderAsyncStub(order);
        }

        public async Task<int> CreateOrderAsyncStub(Order order)
        {
            return await _orderDaoStub.CreateAsync(order);
        }
    }
}
