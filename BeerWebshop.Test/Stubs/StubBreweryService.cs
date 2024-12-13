using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services.Interfaces;

namespace BeerWebshop.Test.Stubs;

public class StubBreweryService : IBreweryService
{
    public Task<int?> GetBreweryIdByName(string name) => Task.FromResult<int?>(1);
    public Task<Brewery?> GetBreweryById(int id) => Task.FromResult(new Brewery { Id = id, Name = "Overtone" });

    public Task<int> CreateBreweryAsync(Brewery brewery)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBreweryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Brewery>> GetBreweriesAsync()
    {
        throw new NotImplementedException();
    }
}
