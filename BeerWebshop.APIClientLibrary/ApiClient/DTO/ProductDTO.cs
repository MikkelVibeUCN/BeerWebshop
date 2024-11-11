using System.ComponentModel;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string BreweryName { get; set; }
        public required float Price { get; set; }
        public required string Description { get; set; }
        public required int Stock { get; set; }
        public float ABV { get; set; }
        public required string CategoryName { get; set; }
        public string? ImageUrl { get; set; }

    }
}
