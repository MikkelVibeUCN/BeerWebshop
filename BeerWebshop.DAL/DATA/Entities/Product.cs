﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.DAL.DATA.Entities
{
    public class Product
    {
        public Product(int? id, string name, int? categoryId_FK, int? breweryId_FK, float price, string description, int stock, float abv, string? imageUrl, bool isDeleted)
        {
            Id = id;
            Name = name;
            CategoryId_FK = categoryId_FK;
            BreweryId_FK = breweryId_FK;
            Price = price;
            Description = description;
            Stock = stock;
            Abv = abv;
            ImageUrl = imageUrl;
            IsDeleted = isDeleted;
           
        }

        public Product()
        {

        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId_FK { get; set; }
        public int? BreweryId_FK { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float Abv { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] RowVersion { get; set; }

        public Category Category { get; set; }
        public Brewery Brewery { get; set; }

    }
}
