using BeerWebshop.APIClientLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.Client
{
    public interface IProductAPIClient
    {
        Task<BeerDTO> GetBeerByIdAsync(int id);
        Task<IEnumerable<BeerDTO>> GetAllBeersAsync();
        Task<int> CreateBeerAsync(BeerDTO entity);
        Task<bool> UpdateBeerAsync(BeerDTO entity);
        Task<bool> DeleteBeerAsync(int id);
        

        
    }
}
