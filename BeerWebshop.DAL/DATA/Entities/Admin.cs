using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class Admin : Account
    {
        public int PermissionLevel { get; set; }
    }
}
