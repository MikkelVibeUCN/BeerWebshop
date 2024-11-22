using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebshop.RESTAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly AccountService _accountService;
    public AccountsController(AccountService accountService)
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

            var token = _accountService.AuthenticateAndGetTokenAsync(new LoginViewModel { Email = viewModel.Email, Password = viewModel.Password });

            if(token == null)
            {
                return Unauthorized("Failed to authenticate after creation");
            }

            return Ok(new { token });
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
        var email = User.Identity?.Name;
        if(string.IsNullOrEmpty(email)) 
        {
            return Unauthorized("User not authenticated");
        }

        try
        {
            var customerDTO = await _accountService.GetByEmail(email);
            return Ok(customerDTO);
        }
        catch (Exception e)
        {
            {
                return BadRequest(e.Message);

            }
        }
    }

    [HttpPost("CustomerLogin")]
    public async Task<ActionResult> AuthenticateLogin([FromBody] LoginViewModel loginView)
    {
        if (loginView == null)
        {
            return BadRequest("No login information provided");
        }
        var token = await _accountService.AuthenticateAndGetTokenAsync(loginView);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Invalid credentials");
        }
        return Ok(new { token });
    }


}
