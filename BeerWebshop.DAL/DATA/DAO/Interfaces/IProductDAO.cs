﻿using BeerWebshop.APIClientLibrary;
using BeerWebshop.DAL.DATA.Entities;
using System.Data.Common;
using System.Data.SqlClient;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IProductDAO : IBaseDAO<Product>
{
    Task<int> CreateAsync(Product entity, SqlConnection? connection = null, DbTransaction? transaction = null);
    Task<int> GetProductCountAsync(ProductQueryParameters parameters);
    Task<IEnumerable<string>> GetProductCategoriesAsync();
    Task<IEnumerable<Product>> GetProducts(ProductQueryParameters parameters);
}