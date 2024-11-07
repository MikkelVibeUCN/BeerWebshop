using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId_FK { get; set; }
        public int? BreweryId_FK { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float Abv { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] RowVersion { get; set; }

        public Category Category { get; set; }
        public Brewery Brewery { get; set; }

    }
}
