using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class Beer
    {
        public string Name { get; set; }
        public string Brewery { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float ABV { get; set; }
        public string Category { get; set; }
        public int? Id { get; set; }
        
        public Beer(string name, string brewery, float price, string description, int stock, float abv, string category, int? id = null)
        {
            Name = name;
            Brewery = brewery;
            Price = price;
            Description = description;
            Stock = stock;
            ABV = abv;
            Category = category;
            Id = id;
        }

    }
}
