using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.Interfaces;

public interface IBeerDAO
{
    Task<int> CreateBeerAsync(Beer beer);
    Task<Beer> GetBeerByIdAsync(int id);
}
