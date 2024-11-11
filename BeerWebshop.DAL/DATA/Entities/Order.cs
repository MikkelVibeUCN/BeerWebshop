using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class Order
    {
        public DateTime Date { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public string? DeliveryAddress { get; set; }
        public bool IsDelivered { get; set; }
        public int? Id { get; set; }
        public bool IsDeleted { get; set; }
        public int? CustomerId_FK { get; set; }

        public float TotalPrice
        {
            get
            {
                return OrderLines.Sum(ol => ol.SubTotal);
            }
        }

        public Order(DateTime date, List<OrderLine> orderLines, bool isDelivered, string? deliveryAddress = null, int? customerId_FK = null, int? id = null)
        {
            Date = date;
            OrderLines = orderLines;
            DeliveryAddress = deliveryAddress;
            IsDelivered = isDelivered;
            Id = id;
            CustomerId_FK = customerId_FK;
            
        }
        public Order() { }

        public void AddOrderLine(OrderLine orderLine)
        {
            OrderLines.Add(orderLine);
        }
        public void RemoveOrderLine(OrderLine orderLine)
        {
            OrderLines.Remove(orderLine);
        }

    }
}
