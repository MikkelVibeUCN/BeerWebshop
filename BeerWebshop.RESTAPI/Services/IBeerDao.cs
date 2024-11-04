using BeerWebshop.RESTAPI.DTO;

namespace BeerWebshop.RESTAPI.Services;

public interface IBeerDao
{
    Task<BeerDTO> GetBearByIdAsync(int id);
    Task<int> CreateBeerAsync(BeerDTO beer);
}
