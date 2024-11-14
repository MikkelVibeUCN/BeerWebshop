using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.Web.Models;
using System.Linq.Expressions;




namespace BeerWebshop.Web.Services
{
    public class AccountService
    {
        private IAccountAPIClient _customerApiClient;

        public AccountService(IAccountAPIClient customerApiClient)
        {
            _customerApiClient = customerApiClient;
        }

        public async Task<int> CreateCustomerAsync(AccountCreationViewModel viewModel)
        {
            try
            {
                string address = $"{viewModel.Street} {viewModel.StreetNumber}";
                if (!string.IsNullOrEmpty(viewModel.ApartmentNumber))
                {
                    address += $" {viewModel.ApartmentNumber}";
                }

                address += $" {viewModel.PostalCode} {viewModel.City}";

                CustomerDTO customer = new CustomerDTO
                {
                    Name = $"{viewModel.FirstName} {viewModel.LastName}",
                    Address = address,
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Phone = viewModel.Phone,
                    Age = viewModel.Age
                };
                return await _customerApiClient.CreateAsync(customer);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating customer: {ex.Message}", ex);
            }

        }
    }
}

                





            
        
