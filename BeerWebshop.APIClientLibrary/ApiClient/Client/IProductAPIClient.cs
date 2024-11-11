using BeerWebshop.APIClientLibrary.ApiClient.DTO;


namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public interface IProductAPIClient
{
    Task<ProductDTO?> GetProductFromIdAsync(int id);
    Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters);
    Task<int> GetProductCountAsync(ProductQueryParameters parameters);
    Task<int> CreateProductAsync(ProductDTO Product);
    Task EditProductAsync(ProductDTO product);
    Task<bool> DeleteProductByIdAsync(int id);

}
