using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<Product?> GetProductEntityByIdAsync(int id);
        Task<int> CreateProductAsync(ProductDTO productDTO);
        Task<List<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters);
        Task<int> GetProductsCount(ProductQueryParameters parameters);
        Task<bool> UpdateProductAsync(ProductDTO productDTO);
        Task<bool> DeleteProductByIdAsync(int id);

    }
}
