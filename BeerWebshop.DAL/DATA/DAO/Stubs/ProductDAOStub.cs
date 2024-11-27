using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.Stubs;

public class ProductDAOStub : IProductDAO
{
    private List<Category> categories = new List<Category>()
    {
        new Category { Id = 1, Name = "Ale" },
        new Category { Id = 2, Name = "Lager" }
    };

    private List<Brewery> _breweries = new List<Brewery>()
    {
        new Brewery { Id = 1, Name = "Brewery A" },
        new Brewery { Id = 2, Name = "Brewery B" }
    };
    private List<Product> _products;
    public ProductDAOStub()
    {
        _products = new List<Product>
    {
        new Product { Id = 1, Name = "Golden Ale", Description = "A crisp and refreshing ale.", Price = 3.99f, Stock = 50, ImageUrl = null, Abv = 5.0f, RowVersion = new byte[8], IsDeleted = false, Category = categories[0], Brewery = _breweries[0] },
        new Product { Id = 2, Name = "Hoppy IPA", Description = "An IPA with bold hoppy flavors.", Price = 4.99f, Stock = 30, ImageUrl = null, Abv = 6.5f, RowVersion = new byte[8], IsDeleted = false, Category = categories[0], Brewery = _breweries[0] },
        new Product { Id = 3, Name = "Dark Stout", Description = "A rich and creamy stout.", Price = 5.49f, Stock = 25, ImageUrl = null, Abv = 8.0f, RowVersion = new byte[8], IsDeleted = false, Category = categories[0], Brewery = _breweries[0] },
        new Product { Id = 4, Name = "Summer Lager", Description = "A light and crisp lager.", Price = 2.99f, Stock = 100, ImageUrl = null, Abv = 4.2f, RowVersion = new byte[8], IsDeleted = false, Category = categories[1], Brewery = _breweries[1] },
        new Product { Id = 5, Name = "Amber Lager", Description = "A malty amber lager.", Price = 3.49f, Stock = 75, ImageUrl = null, Abv = 5.0f, RowVersion = new byte[8], IsDeleted = false, Category = categories[1], Brewery = _breweries[1] },
    };
    }


    public Task<int> CreateAsync(Product entity, SqlConnection? connection = null, DbTransaction? transaction = null)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(Product entity)
    {
        var nextAvailableId = _products.Max(p => p.Id) + 1;
        entity.Id = nextAvailableId;
        _products.Add(entity);
        return await Task.FromResult(entity.Id.Value);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var productToDelete = _products.SingleOrDefault(p => p.Id == id);
        return await Task.FromResult(_products.Remove(productToDelete));
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await Task.FromResult(_products.ToList());
    }

    public async Task<Product>? GetByIdAsync(int id)
    {
        return await Task.FromResult(_products.SingleOrDefault(p =>p.Id == id));
    }

    public async Task<IEnumerable<string>> GetProductCategoriesAsync()
    {
        var categoryNames = _products
       .Select(p => p.Category?.Name) 
       .Where(name => !string.IsNullOrEmpty(name))
       .Distinct(); 

        return await Task.FromResult(categoryNames);
    }

    public async Task<int> GetProductCountAsync(ProductQueryParameters parameters)
    {
        return await Task.FromResult(_products.Count);
    }

    public Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Product entity)
    {
       
        var productToUpdate = _products.SingleOrDefault(p => p.Id == entity.Id);

        if (productToUpdate == null)
        {
            return await Task.FromResult(false);
        }
        productToUpdate.Name = entity.Name;
        productToUpdate.Description = entity.Description;
        productToUpdate.Price = entity.Price;
        productToUpdate.Stock = entity.Stock;
        productToUpdate.ImageUrl = entity.ImageUrl;
        productToUpdate.Abv = entity.Abv;
        productToUpdate.RowVersion = entity.RowVersion;
        productToUpdate.IsDeleted = entity.IsDeleted;
        productToUpdate.Category = entity.Category;
        productToUpdate.Brewery = entity.Brewery;

        return await Task.FromResult(true);
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantity, SqlConnection? connection = null, DbTransaction? transaction = null)
    {
        var productStock = _products.SingleOrDefault(p => productId == p.Id);
        if(productStock == null)
        {
            return await Task.FromResult(false);
        }

        productStock.Stock = quantity;
        return await Task.FromResult(true);
    }
}

