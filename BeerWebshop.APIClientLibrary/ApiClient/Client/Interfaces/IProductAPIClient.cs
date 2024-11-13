using BeerWebshop.APIClientLibrary.ApiClient.DTO;


namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IProductAPIClient
{
    Task<int> CreateAsync(ProductDTO entity, string? endpoint = null);
    Task<bool> DeleteAsync(int id, string? endpoint = null);
    Task<ProductDTO?> GetAsync(int id, string? endpoint = null);
    Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters);
	Task<int> GetProductCountAsync(ProductQueryParameters parameters);
	Task EditProductAsync(ProductDTO product);
	

}
