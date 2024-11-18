using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
