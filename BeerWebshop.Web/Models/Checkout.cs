namespace BeerWebshop.Web.Models
{
    public class Checkout
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Floor { get; set; }
        public string CreditCard { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvc { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}
