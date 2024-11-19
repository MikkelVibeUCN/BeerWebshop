using System.ComponentModel.DataAnnotations;

namespace BeerWebshop.Web.Models
{
    public class Checkout
    {
        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Phonenumber { get; set; }
        [EmailAddress(ErrorMessage = "Indtast en gyldig emailadresse")]
        public string? Email { get; set; }

        [RequiredIfCustomAddress("WantsCustomAddress", ErrorMessage = "Vej mangler")]
        public string? Street { get; set; }

        [RequiredIfCustomAddress("WantsCustomAddress", ErrorMessage = "Telefonnummer mangler.")]
        public string? Number { get; set; }

        [RequiredIfCustomAddress("WantsCustomAddress", ErrorMessage = "Postnummer mangler.")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "Postnummer skal være mellem 4 og 6 tal langt.")]
        public string? PostalCode { get; set; }

        [RequiredIfCustomAddress("WantsCustomAddress", ErrorMessage = "By mangler.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Kortnummer mangler")]
        [CreditCard(ErrorMessage = "Forkert format.")]
        public string? CreditCard { get; set; }

        [Required(ErrorMessage = "Udløbsdato mangler.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", ErrorMessage = "Udløbsdato skal være i MM/YY format.")]
        public string? ExpirationDate { get; set; }

        [Required(ErrorMessage = "CVC mangler.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVC skal være mellem 3 og 4 tal langt.")]
        public string? Cvc { get; set; }
        public bool WantsCustomAddress { get; set; }
    }
}
