using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class OrderLine
    {
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }

		public float SubTotal
		{
			get
			{
				return Quantity * (Product?.Price ?? 0);
			}

		}


		public OrderLine(int quantity, Product product, int productId, int? orderId = null)
        {
            OrderId = orderId;
            Quantity = quantity;
            Product = product;
            ProductId = productId;

        }
        public OrderLine() { }
		
	}
}
