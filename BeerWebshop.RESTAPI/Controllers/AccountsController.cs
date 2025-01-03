﻿using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services.Interfaces;
using BeerWebshop.RESTAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateCustomer([FromBody] AccountCreationViewModel viewModel)
    {
        if (viewModel == null || !ModelState.IsValid)
        {
            return BadRequest("Invalid registration data");
        }

        try
        {
            string address = $"{viewModel.Street} {viewModel.StreetNumber}";
            if (!string.IsNullOrEmpty(viewModel.ApartmentNumber))
            {
                address += $" {viewModel.ApartmentNumber}";
            }

            address += $" {viewModel.PostalCode} {viewModel.City}";

            CustomerDTO customerDTO = new CustomerDTO
            {
                Name = $"{viewModel.FirstName} {viewModel.LastName}",
                Address = address,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Phone = viewModel.Phone,
                Age = viewModel.Age
            };

            Customer customer = MappingHelper.MapToEntity(customerDTO);

            await _accountService.SaveCustomerAsync(customer);

            string? Token = await _accountService.AuthenticateAndGetTokenAsync(new LoginViewModel { Email = viewModel.Email, Password = viewModel.Password });

            if (Token == null)
            {
                return Unauthorized("Failed to authenticate after creation");
            }

            return Ok(new { Token });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetLoggedInCustomer()
    {
        var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return Unauthorized("User not authenticated");
        }
        try
        {
            var accountDTO = await _accountService.GetByEmail(email);
            switch (accountDTO.Role)
            {
                case "User":
                    return Ok(MappingHelper.MapToDTO(accountDTO as Customer));
                case "Admin":
                    return Ok(MappingHelper.MapToDTO(accountDTO as Admin));
                default:
                    return BadRequest("Invalid role");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("CustomerLogin")]
    public async Task<ActionResult> AuthenticateLogin([FromBody] LoginViewModel loginView)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid state");
        }

        var Token = await _accountService.AuthenticateAndGetTokenAsync(loginView);

        if (string.IsNullOrEmpty(Token))
        {
            return Unauthorized("Invalid credentials");
        }
        return Ok(new { Token });
    }
}