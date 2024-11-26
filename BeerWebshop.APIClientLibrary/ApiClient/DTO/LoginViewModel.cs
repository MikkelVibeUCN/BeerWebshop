using System.ComponentModel.DataAnnotations;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email er påkrævet")]
        [EmailAddress(ErrorMessage = "Indtast en gyldig emailadresse")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Adgangskode er påkrævet")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])(?=.*\d)(?=.*[A-Za-z]).{8,}$", ErrorMessage = "Adgangskoden skal indeholde mindst 8 tegn, et specialtegn og et tal")]
        public string Password { get; set; }
    }
}