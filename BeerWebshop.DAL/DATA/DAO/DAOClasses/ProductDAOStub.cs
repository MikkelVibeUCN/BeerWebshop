using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public class ProductDAOStub : IProductDAO
{
    private static readonly List<Product> products = new()
    {
         new Product("Test Beer", "Test Brewery", 2.99f, "Test Description", 100, 5.0f, "Test Category", 1)
    };

    public Task<int> CreateAsync(Product product)
    {
        product.Id = products.Count + 1;
        products.Add(product);
        return Task.FromResult(product.Id.Value);
    }

    public Task<Product> GetByIdAsync(int id)
    {
        return Task.FromResult(products.FirstOrDefault(p => p.Id == id));
    }

    public Task<IEnumerable<Product>> GetFromCategoryAsync(string category)
    {
        throw new NotImplementedException();
    }
}
