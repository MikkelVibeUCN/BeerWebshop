using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
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

    [HttpGet]
    public async Task<ActionResult> GetCustomerFromEmail([FromBody]string email)
    {
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

    private static Customer MapToCustomer(CustomerDTO customer)
    {
        return new Customer
        {
            Name = customer.Name,
            Address = customer.Address,
            Email = customer.Email,
            Phone = customer.Phone,
            Password = customer.Password,
            Age = customer.Age,
        };
    }
}
