using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class OrderLine
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }

		public float SubTotal
		{
			get
			{
				return Quantity * (Product?.Price ?? 0);
			}

		}


		public OrderLine(int quantity, Product product)
        {
            Quantity = quantity;
            Product = product;

        }
        public OrderLine() { }
		
	}
}
