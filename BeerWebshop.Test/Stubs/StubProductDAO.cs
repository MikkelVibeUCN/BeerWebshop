using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using System.Data.Common;
using System.Data.SqlClient;

public class StubProductDAO : IProductDAO
{
    private readonly List<Product> _products = new();

    public Task<int> CreateAsync(Product product)
    {
        product.Id = _products.Count > 0 ? _products.Max(p => p.Id ?? 0) + 1 : 1;
        _products.Add(product);
        return Task.FromResult(product.Id ?? 0);
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<bool> UpdateAsync(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null) return Task.FromResult(false);

        _products.Remove(existingProduct);
        _products.Add(product);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return Task.FromResult(false);

        _products.Remove(product);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_products.AsEnumerable());
    }

    public Task<int> GetProductCountAsync(ProductQueryParameters parameters)
    {
        return Task.FromResult(_products.Count);
    }

    public Task<IEnumerable<string>> GetProductCategoriesAsync()
    {
        var categories = _products.Select(p => p.Category.Name).Distinct();
        return Task.FromResult(categories.AsEnumerable());
    }

    public Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters)
    {
        var filteredProducts = _products.AsQueryable();

        if (!string.IsNullOrEmpty(parameters.Category))
        {
            filteredProducts = filteredProducts.Where(p => p.Category.Name == parameters.Category);
        }

        return Task.FromResult(filteredProducts.AsEnumerable());
    }

    public Task<int> CreateAsync(Product product, SqlConnection? connection = null, DbTransaction? transaction = null)
    {
        product.Id = _products.Count > 0 ? _products.Max(p => p.Id ?? 0) + 1 : 1;
        _products.Add(product);
        return Task.FromResult(product.Id ?? 0);
    }
}
