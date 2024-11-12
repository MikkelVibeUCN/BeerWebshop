﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO customerDTO)
        {
            try
            {
                Customer customer = MapToCustomer(customerDTO);

                int customerId = await _accountService.SaveCustomerAsync(customer);
                return Ok(customerId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private static Customer MapToCustomer(CustomerDTO customer)
        {
            return new Customer
            {
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email,
                Phone = customer.Phone,
                Password = customer.Password,
                ZipCode = customer.ZipCode,
                City = customer.City,
                Age = customer.Age,
            };
        }
    }
}