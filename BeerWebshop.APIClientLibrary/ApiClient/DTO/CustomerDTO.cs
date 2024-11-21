﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class CustomerDTO
    {
        public int? Id { get; set; }
      
        public string Name { get; set; }
        
        public string Address { get; set; } 
        
   
        public string Email { get; set; }
        
        public string? Password { get; set; }
        
        public string Phone { get; set; }
      
        public int? Age { get; set; }

	}
}
