
using BeerWebshop.Web.Validation;
using System.ComponentModel.DataAnnotations;

namespace BeerWebshop.Web.Models
{
    public class AccountCreationViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        public string? ApartmentNumber { get; set; }

        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])(?=.*\d)(?=.*[A-Za-z]).{8,}$", ErrorMessage = "Password must contain 8 letters, one special character and a number")]

        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [MinAge(18)]
        public int Age { get; set; }
    }

}

