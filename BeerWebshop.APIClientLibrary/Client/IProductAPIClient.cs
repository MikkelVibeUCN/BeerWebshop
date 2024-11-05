﻿using BeerWebshop.APIClientLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.Client
{
    public interface IProductAPIClient
    {
        Task<Product> GetBeerByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllBeersAsync();
        Task<int> CreateBeerAsync(ProductDTO entity);
        Task<bool> UpdateBeerAsync(ProductDTO entity);
        Task<bool> DeleteBeerAsync(int id);
        

        
    }
}
