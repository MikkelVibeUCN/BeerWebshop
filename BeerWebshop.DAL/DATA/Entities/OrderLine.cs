using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class OrderLine
    {
        public int? Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public float SubTotal
        {
            get
            {
                return Quantity * Product.Price;
            }
        }

        public OrderLine(int quantity, Product Product, int? id = null)
        {
            Id = id;
            Quantity = quantity;
            Product = Product;
        }
        public OrderLine() { }
    }
}
