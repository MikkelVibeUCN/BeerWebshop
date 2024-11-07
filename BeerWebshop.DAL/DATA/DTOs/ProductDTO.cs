using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Brewery { get; set; }
    public required float Price { get; set; }
    public required string Description { get; set; }
    public required int Stock { get; set; }
    public float ABV { get; set; }
    public required string Type { get; set; }
    public string ImageUrl { get; set; }

}

