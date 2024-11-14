using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerWebshop.APIClientLibrary.Validation;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class CustomerDTO
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; } // Street StreetNumber OptionalApartment ZipCode City
        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
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
