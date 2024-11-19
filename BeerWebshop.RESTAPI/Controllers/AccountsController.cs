using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DAL.DATA.Entities;
using BeerWebshop.RESTAPI.Services;
using BeerWebshop.RESTAPI.Tools;
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
            Customer customer = MappingHelper.MapToEntity(customerDTO);

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

}
