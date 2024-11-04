using BeerWebshop.RESTAPI.DTO;

namespace BeerWebshop.RESTAPI.Services;

public class BeerDaoStub : IBeerDao
{
    private static readonly List<BeerDTO> _beers = new()
    {
        new BeerDTO
        {
            Id = 1,
            Name = "Heineken",
            Price = 1.5m,
            Description = "Heineken is a pale lager beer with 5% alcohol by volume produced by the Dutch brewing company Heineken International.",
            Stock = 100,
            ABV = 5,
            Category = "Lager"
        }
    };

    public Task<int> CreateBeerAsync(BeerDTO beer)
    {
        beer.Id = _beers.Count + 1;
        _beers.Add(beer);
        return Task.FromResult(beer.Id);
    }

    public async Task<BeerDTO?> GetBearByIdAsync(int id)
    {
        return await Task.FromResult(_beers.FirstOrDefault(b => b.Id == id));
    }
}
