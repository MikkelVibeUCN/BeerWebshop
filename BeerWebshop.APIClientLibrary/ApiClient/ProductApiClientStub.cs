
//using BeerWebshop.APIClientLibrary.ApiClient.Client;

//namespace BeerWebshop.APIClientLibrary.ApiClient
//{
//    public class ProductApiClientStub : IProductAPIClient
//    {
//        #region Testdata til øl
//        private static List<ProductDTO> _beers = new List<ProductDTO>()
//        {
//            new ProductDTO { Id = 1, Name = "Golden Lager", BreweryName = "Sunshine Brewery", Price = 5.99f, Description = "A crisp and refreshing lager.", Stock = 120, ABV = 4.5f, CategoryName = "Lager" },
//            new ProductDTO { Id = 2, Name = "Hoppy IPA", BreweryName = "Horizon Hops", Price = 6.99f, Description = "A bold IPA with a hoppy finish.", Stock = 95, ABV = 6.8f, CategoryName = "IPA" },
//            new ProductDTO { Id = 3, Name = "Dark Stout", BreweryName = "Black Barrel Brewery", Price = 7.49f, Description = "Rich and velvety stout with coffee notes.", Stock = 50, ABV = 8.2f, CategoryName = "Stout" },
//            new ProductDTO { Id = 4, Name = "Amber Ale", BreweryName = "Rusty Tap", Price = 5.49f, Description = "A smooth amber ale with caramel undertones.", Stock = 110, ABV = 5.5f, CategoryName = "Ale" },
//            new ProductDTO { Id = 5, Name = "Wheat Wonder", BreweryName = "Grain & Glory", Price = 4.99f, Description = "A light wheat ProductDTO perfect for summer.", Stock = 140, ABV = 4.2f, CategoryName = "Wheat ProductDTO" },
//            new ProductDTO { Id = 6, Name = "Tropical Pale Ale", BreweryName = "Island Brews", Price = 6.49f, Description = "Pale ale infused with tropical fruits.", Stock = 70, ABV = 5.6f, CategoryName = "Pale Ale" },
//            new ProductDTO { Id = 7, Name = "Double IPA", BreweryName = "Hopped Heights", Price = 8.99f, Description = "Strong double IPA with intense hop flavor.", Stock = 40, ABV = 9.0f, CategoryName = "Double IPA" },
//            new ProductDTO { Id = 8, Name = "Belgian Tripel", BreweryName = "Old Abbey Brewery", Price = 9.49f, Description = "Traditional Belgian tripel with a hint of spice.", Stock = 55, ABV = 9.5f, CategoryName = "Tripel" },
//            new ProductDTO { Id = 9, Name = "Pilsner Pride", BreweryName = "Classic Brews", Price = 4.79f, Description = "A clean and crisp pilsner.", Stock = 130, ABV = 4.7f, CategoryName = "Pilsner" },
//            new ProductDTO { Id = 10, Name = "Sour Cherry Ale", BreweryName = "Wild Flavors", Price = 6.29f, Description = "Tart ale with cherry flavors.", Stock = 75, ABV = 5.2f, CategoryName = "Sour Ale" },
//            new ProductDTO { Id = 11, Name = "Session IPA", BreweryName = "Craft Pour", Price = 5.99f, Description = "Light IPA with lower alcohol content.", Stock = 90, ABV = 4.1f, CategoryName = "Session IPA" },
//            new ProductDTO { Id = 12, Name = "Smoky Porter", BreweryName = "Barrel Aged Co.", Price = 7.29f, Description = "Smoky porter with deep roasted flavor.", Stock = 60, ABV = 6.2f, CategoryName = "Porter" },
//            new ProductDTO { Id = 13, Name = "Honey Brown Ale", BreweryName = "Bee's Best", Price = 5.89f, Description = "Ale with a touch of honey sweetness.", Stock = 100, ABV = 5.0f, CategoryName = "Brown Ale" },
//            new ProductDTO { Id = 14, Name = "Pumpkin Spice Ale", BreweryName = "Seasonal Sips", Price = 5.99f, Description = "A fall favorite with pumpkin and spice.", Stock = 65, ABV = 5.8f, CategoryName = "Spiced Ale" },
//            new ProductDTO { Id = 15, Name = "Citrus Haze", BreweryName = "Cloudy Brews", Price = 6.79f, Description = "Hazy IPA with citrus notes.", Stock = 85, ABV = 6.4f, CategoryName = "Hazy IPA" },
//            new ProductDTO { Id = 16, Name = "Barrel-Aged Stout", BreweryName = "Oak & Malt", Price = 10.49f, Description = "Stout aged in whiskey barrels.", Stock = 30, ABV = 10.2f, CategoryName = "Stout" },
//            new ProductDTO { Id = 17, Name = "Light Lager", BreweryName = "Breezy Brews", Price = 3.99f, Description = "Low-calorie lager for easy drinking.", Stock = 200, ABV = 3.8f, CategoryName = "Light Lager" },
//            new ProductDTO { Id = 18, Name = "Red Rye Ale", BreweryName = "Red Harvest", Price = 6.59f, Description = "Spicy rye ale with a deep red color.", Stock = 80, ABV = 5.9f, CategoryName = "Rye Ale" },
//            new ProductDTO { Id = 19, Name = "Mango Hefeweizen", BreweryName = "Tropical Taste", Price = 5.79f, Description = "Hefeweizen with mango flavors.", Stock = 115, ABV = 5.3f, CategoryName = "Hefeweizen" },
//            new ProductDTO { Id = 20, Name = "Coconut Porter", BreweryName = "Island Brews", Price = 7.19f, Description = "Porter with a hint of coconut.", Stock = 45, ABV = 6.7f, CategoryName = "Porter" },
//            new ProductDTO { Id = 1, Name = "Golden Lager", BreweryName = "Sunshine Brewery", Price = 5.99f, Description = "A crisp and refreshing lager.", Stock = 120, ABV = 4.5f, CategoryName = "Lager" },
//            new ProductDTO { Id = 2, Name = "Dark Stout", BreweryName = "Moonlight Brewing Co.", Price = 6.99f, Description = "A rich and robust stout with hints of chocolate and coffee.", Stock = 85, ABV = 7.2f, CategoryName = "Stout" },
//            new ProductDTO { Id = 3, Name = "Hoppy IPA", BreweryName = "Mountain Peak Brewery", Price = 7.49f, Description = "A bold IPA with strong hop bitterness and citrus notes.", Stock = 150, ABV = 6.8f, CategoryName = "IPA" },
//            new ProductDTO { Id = 4, Name = "Amber Ale", BreweryName = "Redwood Brewing", Price = 5.49f, Description = "A smooth amber ale with a caramel malt flavor.", Stock = 110, ABV = 5.2f, CategoryName = "Ale" },
//            new ProductDTO { Id = 5, Name = "Wheat Bliss", BreweryName = "Sunny Valley Brewing", Price = 6.29f, Description = "A light and refreshing wheat beer with fruity flavors.", Stock = 90, ABV = 4.2f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 6, Name = "Citrus Burst", BreweryName = "Cascade Brew Works", Price = 6.79f, Description = "An IPA with a punch of grapefruit and citrus zest.", Stock = 140, ABV = 7.5f, CategoryName = "IPA" },
//            new ProductDTO { Id = 7, Name = "Bitter Brew", BreweryName = "Evergreen Brewing", Price = 5.99f, Description = "A bitter English pale ale with earthy hop flavors.", Stock = 100, ABV = 5.6f, CategoryName = "Pale Ale" },
//            new ProductDTO { Id = 8, Name = "Cherry Sour", BreweryName = "Bramble Brewing", Price = 8.49f, Description = "A tart and refreshing sour beer with cherry infusion.", Stock = 60, ABV = 4.0f, CategoryName = "Sour" },
//            new ProductDTO { Id = 9, Name = "Imperial Red", BreweryName = "Dark Horse Brewing", Price = 9.99f, Description = "A bold imperial red ale with malty sweetness and a hoppy finish.", Stock = 45, ABV = 8.1f, CategoryName = "Red Ale" },
//            new ProductDTO { Id = 10, Name = "Blonde Ale", BreweryName = "Golden Fields Brewery", Price = 5.69f, Description = "A light, smooth blonde ale with a subtle malt flavor.", Stock = 125, ABV = 4.3f, CategoryName = "Blonde Ale" },
//            new ProductDTO { Id = 11, Name = "Tropical IPA", BreweryName = "Blue Horizon Brewing", Price = 7.99f, Description = "A tropical IPA with mango, pineapple, and citrus.", Stock = 130, ABV = 6.5f, CategoryName = "IPA" },
//            new ProductDTO { Id = 12, Name = "Maple Stout", BreweryName = "Timberwood Brewing", Price = 8.29f, Description = "A rich stout brewed with real maple syrup.", Stock = 75, ABV = 7.9f, CategoryName = "Stout" },
//            new ProductDTO { Id = 13, Name = "Crisp Pilsner", BreweryName = "Silver Creek Brewing", Price = 5.79f, Description = "A refreshing pilsner with a clean finish.", Stock = 145, ABV = 4.3f, CategoryName = "Pilsner" },
//            new ProductDTO { Id = 14, Name = "Berry Wheat", BreweryName = "Wildflower Brewing", Price = 6.59f, Description = "A wheat beer infused with fresh berries.", Stock = 85, ABV = 5.0f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 15, Name = "Vanilla Porter", BreweryName = "Seaside Brewing", Price = 7.29f, Description = "A smooth porter with vanilla and chocolate flavors.", Stock = 50, ABV = 6.1f, CategoryName = "Porter" },
//            new ProductDTO { Id = 16, Name = "Cocoa Stout", BreweryName = "Night Owl Brewery", Price = 8.99f, Description = "A rich stout with cocoa and a hint of coffee.", Stock = 110, ABV = 7.8f, CategoryName = "Stout" },
//            new ProductDTO { Id = 17, Name = "Citrus Blonde", BreweryName = "Riverside Brewing", Price = 5.89f, Description = "A crisp and light blonde ale with a hint of citrus.", Stock = 130, ABV = 4.2f, CategoryName = "Blonde Ale" },
//            new ProductDTO { Id = 18, Name = "Peach IPA", BreweryName = "Sunset Brewing", Price = 6.99f, Description = "A juicy IPA with peach and tropical flavors.", Stock = 120, ABV = 6.0f, CategoryName = "IPA" },
//            new ProductDTO { Id = 19, Name = "Honey Wheat", BreweryName = "Golden Valley Brewing", Price = 5.49f, Description = "A wheat beer brewed with honey for a sweet, smooth finish.", Stock = 90, ABV = 4.5f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 20, Name = "Smoked Porter", BreweryName = "Bluff City Brewing", Price = 7.59f, Description = "A rich porter with smoky flavors.", Stock = 60, ABV = 7.2f, CategoryName = "Porter" },
//            new ProductDTO { Id = 21, Name = "Apple Cider Ale", BreweryName = "Whispering Pines Brewing", Price = 6.39f, Description = "A unique blend of apple cider and ale.", Stock = 85, ABV = 4.7f, CategoryName = "Cider Ale" },
//            new ProductDTO { Id = 22, Name = "Berry Saison", BreweryName = "Crystal Springs Brewing", Price = 7.99f, Description = "A refreshing saison with a hint of berries.", Stock = 70, ABV = 5.5f, CategoryName = "Saison" },
//            new ProductDTO { Id = 23, Name = "Red IPA", BreweryName = "Rogue Waters Brewing", Price = 8.49f, Description = "A red IPA with malty sweetness and a hoppy bitterness.", Stock = 80, ABV = 6.8f, CategoryName = "IPA" },
//            new ProductDTO { Id = 24, Name = "Coconut Stout", BreweryName = "Beachside Brewing", Price = 8.19f, Description = "A rich stout with toasted coconut flavors.", Stock = 100, ABV = 7.4f, CategoryName = "Stout" },
//            new ProductDTO { Id = 25, Name = "Lemon Lager", BreweryName = "Northern Lights Brewing", Price = 5.79f, Description = "A light lager with a zesty lemon twist.", Stock = 125, ABV = 4.2f, CategoryName = "Lager" },
//            new ProductDTO { Id = 26, Name = "Caramel Amber", BreweryName = "Golden Peak Brewing", Price = 6.49f, Description = "An amber ale with a rich caramel malt flavor.", Stock = 95, ABV = 5.3f, CategoryName = "Amber Ale" },
//            new ProductDTO { Id = 27, Name = "Blackberry Wheat", BreweryName = "Stone Ridge Brewing", Price = 6.79f, Description = "A smooth wheat beer with a hint of blackberry.", Stock = 110, ABV = 5.0f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 28, Name = "Pumpkin Ale", BreweryName = "Autumn Harvest Brewing", Price = 6.99f, Description = "A seasonal pumpkin ale with spice and malty sweetness.", Stock = 50, ABV = 5.8f, CategoryName = "Ale" },
//            new ProductDTO { Id = 29, Name = "Ginger Lager", BreweryName = "Silver Peak Brewery", Price = 5.59f, Description = "A zesty lager with a hint of ginger.", Stock = 140, ABV = 4.3f, CategoryName = "Lager" },
//            new ProductDTO { Id = 30, Name = "Mango IPA", BreweryName = "Tropical Breeze Brewing", Price = 7.99f, Description = "A juicy IPA bursting with mango flavors.", Stock = 120, ABV = 6.2f, CategoryName = "IPA" },
//            new ProductDTO { Id = 31, Name = "Pineapple Wheat", BreweryName = "Coastal Brewing", Price = 6.19f, Description = "A tropical wheat beer with pineapple flavor.", Stock = 115, ABV = 5.2f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 32, Name = "Sour Cherry", BreweryName = "Crimson River Brewing", Price = 7.89f, Description = "A tart and fruity sour cherry beer.", Stock = 85, ABV = 4.1f, CategoryName = "Sour" },
//            new ProductDTO { Id = 33, Name = "Cranberry IPA", BreweryName = "Red Stone Brewing", Price = 7.49f, Description = "A tart IPA with cranberry and citrus notes.", Stock = 105, ABV = 6.4f, CategoryName = "IPA" },
//            new ProductDTO { Id = 34, Name = "Tangerine Pale Ale", BreweryName = "East Coast Brewing", Price = 6.79f, Description = "A pale ale with a refreshing tangerine twist.", Stock = 120, ABV = 5.5f, CategoryName = "Pale Ale" },
//            new ProductDTO { Id = 35, Name = "Belgian Dubbel", BreweryName = "Old World Brewing", Price = 8.49f, Description = "A rich Belgian dubbel with fruity and malty flavors.", Stock = 65, ABV = 7.4f, CategoryName = "Belgian" },
//            new ProductDTO { Id = 36, Name = "Grapefruit IPA", BreweryName = "Wild Rivers Brewing", Price = 7.29f, Description = "A bold IPA with a burst of grapefruit bitterness.", Stock = 140, ABV = 6.9f, CategoryName = "IPA" },
//            new ProductDTO { Id = 37, Name = "Honey Amber", BreweryName = "Northern Star Brewing", Price = 6.89f, Description = "An amber ale with a smooth honey finish.", Stock = 90, ABV = 5.1f, CategoryName = "Amber Ale" },
//            new ProductDTO { Id = 38, Name = "Chocolate Milk Stout", BreweryName = "Sweet Harmony Brewing", Price = 8.99f, Description = "A decadent stout with chocolate milk flavor.", Stock = 50, ABV = 7.6f, CategoryName = "Stout" },
//            new ProductDTO { Id = 39, Name = "Raspberry Wheat", BreweryName = "River Bend Brewing", Price = 6.59f, Description = "A fruity wheat beer with raspberry notes.", Stock = 110, ABV = 5.0f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 40, Name = "Maple Brown Ale", BreweryName = "Forest Oak Brewing", Price = 7.19f, Description = "A malty brown ale with a touch of maple sweetness.", Stock = 75, ABV = 5.8f, CategoryName = "Brown Ale" },
//            new ProductDTO { Id = 41, Name = "Lime Lager", BreweryName = "Pacific Coast Brewing", Price = 5.49f, Description = "A light lager with a refreshing lime twist.", Stock = 150, ABV = 4.1f, CategoryName = "Lager" },
//            new ProductDTO { Id = 42, Name = "Lemon Wheat", BreweryName = "Sunset Ridge Brewing", Price = 6.39f, Description = "A crisp wheat beer with lemon flavor.", Stock = 130, ABV = 4.8f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 43, Name = "Rosemary IPA", BreweryName = "Herb Garden Brewing", Price = 7.79f, Description = "A unique IPA with rosemary and herbal flavors.", Stock = 95, ABV = 6.7f, CategoryName = "IPA" },
//            new ProductDTO { Id = 44, Name = "Lavender Ale", BreweryName = "Flower Field Brewing", Price = 6.59f, Description = "An ale with subtle lavender flavor.", Stock = 100, ABV = 5.3f, CategoryName = "Ale" },
//            new ProductDTO { Id = 45, Name = "Apricot Stout", BreweryName = "Stonefruit Brewing", Price = 7.89f, Description = "A stout with the sweetness of apricot.", Stock = 80, ABV = 7.3f, CategoryName = "Stout" },
//            new ProductDTO { Id = 46, Name = "Citrus Pale Ale", BreweryName = "Yellow Rock Brewing", Price = 6.79f, Description = "A pale ale with bright citrus flavors.", Stock = 140, ABV = 5.2f, CategoryName = "Pale Ale" },
//            new ProductDTO { Id = 47, Name = "Pomegranate Wheat", BreweryName = "Crimson Valley Brewing", Price = 6.99f, Description = "A refreshing wheat beer with pomegranate notes.", Stock = 85, ABV = 4.8f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 48, Name = "Mango Pale Ale", BreweryName = "Tropical Coast Brewing", Price = 6.49f, Description = "A pale ale with tropical mango flavor.", Stock = 120, ABV = 5.4f, CategoryName = "Pale Ale" },
//            new ProductDTO { Id = 49, Name = "Saffron IPA", BreweryName = "Golden Spire Brewing", Price = 7.59f, Description = "An IPA brewed with saffron for a unique flavor.", Stock = 100, ABV = 7.1f, CategoryName = "IPA" },
//            new ProductDTO { Id = 50, Name = "Passionfruit Pale Ale", BreweryName = "Island Breeze Brewing", Price = 6.79f, Description = "A pale ale with passionfruit infusion.", Stock = 110, ABV = 5.3f, CategoryName = "Pale Ale" },
//            new ProductDTO { Id = 51, Name = "Cranberry Wheat", BreweryName = "Wild Harvest Brewing", Price = 6.39f, Description = "A wheat beer with tart cranberry flavor.", Stock = 80, ABV = 5.0f, CategoryName = "Wheat" },
//            new ProductDTO { Id = 52, Name = "Fennel Lager", BreweryName = "Mountain Springs Brewing", Price = 5.99f, Description = "A lager with a touch of fennel for a unique twist.", Stock = 140, ABV = 4.2f, CategoryName = "Lager" },
//            new ProductDTO { Id = 53, Name = "Tangerine Stout", BreweryName = "Horizon Brewing", Price = 8.39f, Description = "A stout with hints of tangerine.", Stock = 55, ABV = 7.2f, CategoryName = "Stout" },
//            new ProductDTO { Id = 54, Name = "Lemon Ginger Pale Ale", BreweryName = "Sunshine Grove Brewing", Price = 6.99f, Description = "A pale ale brewed with lemon and ginger.", Stock = 95, ABV = 5.1f, CategoryName = "Pale Ale" },
//            new ProductDTO { Id = 55, Name = "Blackberry Porter", BreweryName = "Tangled Roots Brewing", Price = 7.69f, Description = "A porter with a rich blackberry infusion.", Stock = 100, ABV = 6.8f, CategoryName = "Porter" }
//        };
//        #endregion

//        private int AddNewBeer(ProductDTO productDTO)
//        {
//            var nextAvailableId = _beers.Max(post => post.Id) + 1;
//            productDTO.Id = nextAvailableId;
//            _beers.Add(productDTO);
//            return productDTO.Id;
//        }

//        public Task<ProductDTO?> GetProductFromIdAsync(int id)
//        {
//            var product = _beers.FirstOrDefault(productDTO => productDTO.Id == id);
//            return Task.FromResult(product);
//        }

//        public Task<IEnumerable<string>> GetProductCategoriesAsync()
//        {
//            return Task.FromResult(_beers.Select(beer => beer.CategoryName).Distinct());
//        }

//        public Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters)
//        {
//            var filteredBeers = _beers.AsQueryable();

//            if (!string.IsNullOrEmpty(parameters.Category))
//            {
//                filteredBeers = filteredBeers.Where(beer => beer.CategoryName.Equals(parameters.Category, StringComparison.OrdinalIgnoreCase));
//            }

//            filteredBeers = parameters.SortBy?.ToLower() switch
//            {
//                "nameasc" => filteredBeers.OrderBy(beer => beer.Name),
//                "namedesc" => filteredBeers.OrderByDescending(beer => beer.Name),
//                "priceasc" => filteredBeers.OrderBy(beer => beer.Price),
//                "pricedesc" => filteredBeers.OrderByDescending(beer => beer.Price),
//                _ => filteredBeers
//            };

//            int skip = parameters.PageNumber * parameters.PageSize;

//            // Apply pagination and convert the result to a List to materialize the query
//            var result = filteredBeers.Skip(skip).Take(parameters.PageSize).ToList();

//            return Task.FromResult(result.AsEnumerable());
//        }

//        public Task<int> GetProductCountAsync(ProductQueryParameters parameters)
//        {
//            var filteredBeers = _beers.AsQueryable();

//            if (!string.IsNullOrEmpty(parameters.Category))
//            {
//                filteredBeers = filteredBeers.Where(beer => beer.CategoryName.Equals(parameters.Category, StringComparison.OrdinalIgnoreCase));
//            }

//            return Task.FromResult(filteredBeers.Count());
//        }

//        public Task<int> CreateProductAsync(ProductDTO ProductDTO)
//        {
//            return Task.FromResult(AddNewBeer(ProductDTO));
//        }

//		public async Task<bool> DeleteProductByIdAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//		public Task EditProductAsync(ProductDTO product)
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
