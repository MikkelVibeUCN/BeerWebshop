using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary
{
    public class ProductQueryParameters
    {
        public string SortBy { get; set; }
        public string Category { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 20;
    }
}
