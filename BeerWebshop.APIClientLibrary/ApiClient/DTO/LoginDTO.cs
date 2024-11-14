using System.ComponentModel.DataAnnotations;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
