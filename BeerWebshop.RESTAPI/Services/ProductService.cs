using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services
{
    public class ProductService
    {
        private readonly IProductDAO _productDAO;
        public ProductService(IProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productDAO.GetByIdAsync(id);
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            return await _productDAO.CreateAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters)
        {
            return await _productDAO.GetProducts(parameters);
        }

		public async Task<bool> UpdateStockAsync(int productId, int quantity, byte[] rowVersion)
        {
            return await _productDAO.UpdateStockOptimisticAsync(productId, quantity, rowVersion);
		}

        public async Task<int> GetProductsCount(ProductQueryParameters parameters)
        {
            return await _productDAO.GetProductCountAsync(parameters);
        }
	}
}
