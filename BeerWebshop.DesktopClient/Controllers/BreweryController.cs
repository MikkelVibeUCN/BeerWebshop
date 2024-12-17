using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.DesktopClient.Controllers
{
    public class BreweryController
    {
        private readonly IBreweryAPIClient _breweryApiClient;
        public BreweryController(BreweryAPIClient breweryAPIClient)
        {
            _breweryApiClient = breweryAPIClient;
        }

        public async Task<IEnumerable<BreweryDTO>> GetAllBreweries()
        {
            return await _breweryApiClient.GetAllAsync();
        }
    }
}
