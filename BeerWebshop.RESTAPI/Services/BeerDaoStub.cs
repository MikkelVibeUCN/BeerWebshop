using BeerWebshop.RESTAPI.DTO;

namespace BeerWebshop.RESTAPI.Services;

public class BeerDaoStub : IBeerDao
{
    private static readonly List<BeerDTO> _beers = new();

    public Task<int> CreateBeerAsync(BeerDTO beer)
    {
        beer.Id = _beers.Count + 1;
        _beers.Add(beer);
        return Task.FromResult(beer.Id);
    }

    public Task<BeerDTO> GetBearByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
