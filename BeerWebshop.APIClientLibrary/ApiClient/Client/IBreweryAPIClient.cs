using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public interface IBreweryAPIClient
{
	Task<int> CreateBreweryAsync(BreweryDTO brewery);
	Task<bool> DeleteAsync(int id);
}