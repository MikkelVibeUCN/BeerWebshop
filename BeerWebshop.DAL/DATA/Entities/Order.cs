﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class Order
    {
        public DateTime Date { get; set; }
        private List<OrderLine> OrderLines { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsDelivered { get; set; }
        public int? Id { get; set; }

        public float TotalPrice
        {
            get
            {
                return OrderLines.Sum(ol => ol.SubTotal);
            }
        }

        public Order(DateTime date, List<OrderLine> orderLines, string deliveryAddress, bool isDelivered, int? id = null)
        {
            Date = date;
            OrderLines = orderLines;
            DeliveryAddress = deliveryAddress;
            IsDelivered = isDelivered;
            Id = id;
        }

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
