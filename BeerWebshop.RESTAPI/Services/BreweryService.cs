﻿using BeerWebshop.DAL.DATA.DAO.Interfaces;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services.Interfaces;

namespace BeerWebshop.RESTAPI.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryDAO _breweryDAO;
        public BreweryService(IBreweryDAO breweryDAO)
        {
            _breweryDAO = breweryDAO;
        }
        public async Task<int> CreateBreweryAsync(Brewery brewery)
        {
            return await _breweryDAO.CreateAsync(brewery);
        }
        public async Task<int?> GetBreweryIdByName(string name)
        {
            return await _breweryDAO.GetBreweryIdByName(name);
        }

        public async Task<Brewery?> GetBreweryById(int id)
        {
            return await _breweryDAO.GetByIdAsync(id);
        }

        public async Task<bool> DeleteBreweryAsync(int id)
        {
            return await _breweryDAO.DeleteAsync(id);
        }

        public async Task<IEnumerable<Brewery>> GetBreweriesAsync()
        {
            return await _breweryDAO.GetAllAsync();
        }
    }
}
