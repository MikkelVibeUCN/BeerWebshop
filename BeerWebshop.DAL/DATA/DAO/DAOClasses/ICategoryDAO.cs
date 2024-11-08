using BeerWebshop.DAL.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.DAO.DAOClasses;

public interface ICategoryDAO
{
	Task<int> CreateCategoryAsync(Category category);
}
