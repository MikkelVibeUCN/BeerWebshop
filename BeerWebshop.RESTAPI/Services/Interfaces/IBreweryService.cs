using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.RESTAPI.Services.Interfaces;

public interface IBreweryService
{
    public Task<int> CreateBreweryAsync(Brewery brewery);
    public Task<int?> GetBreweryIdByName(string name);
    public Task<Brewery?> GetBreweryById(int id);
    public Task<bool> DeleteBreweryAsync(int id);
    public Task<IEnumerable<Brewery>> GetBreweriesAsync();

}
