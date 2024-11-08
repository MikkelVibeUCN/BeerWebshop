﻿using BeerWebshop.APIClientLibrary;
using BeerWebshop.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.Web.Controllers
{
    public class BeerController : Controller
    {
        private readonly BeerService _beerService;

        public BeerController(BeerService beerService)
        {
            _beerService = beerService;
        }

        // GET: BeerController
        public async Task<IActionResult> Index(ProductQueryParameters parameters)
        {
            ViewBag.Categories = await _beerService.GetProductCategories();

            int totalProductCount = await _beerService.GetProductCount(parameters);
            int totalPages = (int)Math.Ceiling(totalProductCount / (double)parameters.PageSize);

            if(totalPages > 1)
            {
                totalPages--;
            }

            ViewBag.CurrentSortOrder = parameters.SortBy;
            ViewBag.CurrentCategory = parameters.Category;
            ViewBag.CurrentPage = parameters.PageNumber;
            ViewBag.TotalPages = totalPages;

            IEnumerable<ProductDTO> beers = await _beerService.GetProducts(parameters);

            return View(beers);
        }

        // GET: BeerController/Details/5
        public ActionResult Details(int id)
        {
            ProductDTO? product = _beerService.GetProductFromId(id).Result;
            if(product != null)
            {
                return View(product);
            }
            return BadRequest();
        }
    }
}