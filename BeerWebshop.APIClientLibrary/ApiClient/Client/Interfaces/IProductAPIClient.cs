using BeerWebshop.APIClientLibrary.ApiClient.DTO;


namespace BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;

public interface IProductAPIClient
{
	Task<int> CreateProductAsync(ProductDTO Product);
	Task<ProductDTO?> GetProductFromIdAsync(int id);
	Task<IEnumerable<ProductDTO>> GetProductsAsync(ProductQueryParameters parameters);
	Task<int> GetProductCountAsync(ProductQueryParameters parameters);
	Task EditProductAsync(ProductDTO product);
	Task<bool> DeleteProductByIdAsync(int id);

}
