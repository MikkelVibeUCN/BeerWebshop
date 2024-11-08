
using BeerWebshop.APIClientLibrary.ApiClient.Client;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class ProductApiClientStub : IProductAPIClient
    {
        #region Testdata til øl
        private static List<ProductDTO> _beers = new List<ProductDTO>()
        {
            new ProductDTO { Id = 1, Name = "Golden Lager", Brewery = "Sunshine Brewery", Price = 5.99f, Description = "A crisp and refreshing lager.", Stock = 120, ABV = 4.5f, Type = "Lager" },
            new ProductDTO { Id = 2, Name = "Hoppy IPA", Brewery = "Horizon Hops", Price = 6.99f, Description = "A bold IPA with a hoppy finish.", Stock = 95, ABV = 6.8f, Type = "IPA" },
            new ProductDTO { Id = 3, Name = "Dark Stout", Brewery = "Black Barrel Brewery", Price = 7.49f, Description = "Rich and velvety stout with coffee notes.", Stock = 50, ABV = 8.2f, Type = "Stout" },
            new ProductDTO { Id = 4, Name = "Amber Ale", Brewery = "Rusty Tap", Price = 5.49f, Description = "A smooth amber ale with caramel undertones.", Stock = 110, ABV = 5.5f, Type = "Ale" },
            new ProductDTO { Id = 5, Name = "Wheat Wonder", Brewery = "Grain & Glory", Price = 4.99f, Description = "A light wheat ProductDTO perfect for summer.", Stock = 140, ABV = 4.2f, Type = "Wheat ProductDTO" },
            new ProductDTO { Id = 6, Name = "Tropical Pale Ale", Brewery = "Island Brews", Price = 6.49f, Description = "Pale ale infused with tropical fruits.", Stock = 70, ABV = 5.6f, Type = "Pale Ale" },
            new ProductDTO { Id = 7, Name = "Double IPA", Brewery = "Hopped Heights", Price = 8.99f, Description = "Strong double IPA with intense hop flavor.", Stock = 40, ABV = 9.0f, Type = "Double IPA" },
            new ProductDTO { Id = 8, Name = "Belgian Tripel", Brewery = "Old Abbey Brewery", Price = 9.49f, Description = "Traditional Belgian tripel with a hint of spice.", Stock = 55, ABV = 9.5f, Type = "Tripel" },
            new ProductDTO { Id = 9, Name = "Pilsner Pride", Brewery = "Classic Brews", Price = 4.79f, Description = "A clean and crisp pilsner.", Stock = 130, ABV = 4.7f, Type = "Pilsner" },
            new ProductDTO { Id = 10, Name = "Sour Cherry Ale", Brewery = "Wild Flavors", Price = 6.29f, Description = "Tart ale with cherry flavors.", Stock = 75, ABV = 5.2f, Type = "Sour Ale" },
            new ProductDTO { Id = 11, Name = "Session IPA", Brewery = "Craft Pour", Price = 5.99f, Description = "Light IPA with lower alcohol content.", Stock = 90, ABV = 4.1f, Type = "Session IPA" },
            new ProductDTO { Id = 12, Name = "Smoky Porter", Brewery = "Barrel Aged Co.", Price = 7.29f, Description = "Smoky porter with deep roasted flavor.", Stock = 60, ABV = 6.2f, Type = "Porter" },
            new ProductDTO { Id = 13, Name = "Honey Brown Ale", Brewery = "Bee's Best", Price = 5.89f, Description = "Ale with a touch of honey sweetness.", Stock = 100, ABV = 5.0f, Type = "Brown Ale" },
            new ProductDTO { Id = 14, Name = "Pumpkin Spice Ale", Brewery = "Seasonal Sips", Price = 5.99f, Description = "A fall favorite with pumpkin and spice.", Stock = 65, ABV = 5.8f, Type = "Spiced Ale" },
            new ProductDTO { Id = 15, Name = "Citrus Haze", Brewery = "Cloudy Brews", Price = 6.79f, Description = "Hazy IPA with citrus notes.", Stock = 85, ABV = 6.4f, Type = "Hazy IPA" },
            new ProductDTO { Id = 16, Name = "Barrel-Aged Stout", Brewery = "Oak & Malt", Price = 10.49f, Description = "Stout aged in whiskey barrels.", Stock = 30, ABV = 10.2f, Type = "Stout" },
            new ProductDTO { Id = 17, Name = "Light Lager", Brewery = "Breezy Brews", Price = 3.99f, Description = "Low-calorie lager for easy drinking.", Stock = 200, ABV = 3.8f, Type = "Light Lager" },
            new ProductDTO { Id = 18, Name = "Red Rye Ale", Brewery = "Red Harvest", Price = 6.59f, Description = "Spicy rye ale with a deep red color.", Stock = 80, ABV = 5.9f, Type = "Rye Ale" },
            new ProductDTO { Id = 19, Name = "Mango Hefeweizen", Brewery = "Tropical Taste", Price = 5.79f, Description = "Hefeweizen with mango flavors.", Stock = 115, ABV = 5.3f, Type = "Hefeweizen" },
            new ProductDTO { Id = 20, Name = "Coconut Porter", Brewery = "Island Brews", Price = 7.19f, Description = "Porter with a hint of coconut.", Stock = 45, ABV = 6.7f, Type = "Porter" }
        };
        #endregion

        private int AddNewBeer(ProductDTO productDTO)
        {
            var nextAvailableId = _beers.Max(post => post.Id) + 1;
            productDTO.Id = nextAvailableId;
            _beers.Add(productDTO);
            return productDTO.Id;
        }

        public Task<ProductDTO?> GetProductFromIdAsync(int id)
        {
            var product = _beers.FirstOrDefault(productDTO => productDTO.Id == id);
            return Task.FromResult(product);
        }

        public Task<IEnumerable<string>> GetProductCategoriesAsync()
        {
            return Task.FromResult(_beers.Select(beer => beer.Type).Distinct());
        }

        public Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var filteredBeers = _beers.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Category))
            {
                filteredBeers = filteredBeers.Where(beer => beer.Type.Equals(parameters.Category, StringComparison.OrdinalIgnoreCase));
            }

            filteredBeers = parameters.SortBy?.ToLower() switch
            {
                "nameasc" => filteredBeers.OrderBy(beer => beer.Name),
                "namedesc" => filteredBeers.OrderByDescending(beer => beer.Name),
                "priceasc" => filteredBeers.OrderBy(beer => beer.Price),
                "pricedesc" => filteredBeers.OrderByDescending(beer => beer.Price),
                _ => filteredBeers
            };

            int skip = parameters.PageNumber * parameters.PageSize;

            // Apply pagination and convert the result to a List to materialize the query
            var result = filteredBeers.Skip(skip).Take(parameters.PageSize).ToList();

            return Task.FromResult(result.AsEnumerable());
        }


        public Task<int> CreateProductAsync(ProductDTO ProductDTO)
        {
            return Task.FromResult(AddNewBeer(ProductDTO));
        }
    }
}
