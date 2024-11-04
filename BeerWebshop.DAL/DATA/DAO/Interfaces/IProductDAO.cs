using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IProductDAO
{
    Task<int> CreateAsync(Product beer);
    Task<Product> GetByIdAsync(int id);
}
