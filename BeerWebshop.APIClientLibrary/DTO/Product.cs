using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.DTO
{
    public class Product
    {
        public string Name { get; set; }
        public string Brewery { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float ABV { get; set; }
        public string Category { get; set; }
        public int? Id { get; set; }
        public string Url { get; set; }
    }
}
