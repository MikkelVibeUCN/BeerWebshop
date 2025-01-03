﻿using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using RestSharp;

namespace BeerWebshop.APIClientLibrary.ApiClient.Client;

public class BreweryAPIClient : BaseClient<BreweryDTO>, IBreweryAPIClient
{
	public BreweryAPIClient(string uri) : base(uri, "Breweries") { }
}