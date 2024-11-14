using System.ComponentModel.DataAnnotations;

namespace BeerWebshop.Web.Validation
{
    public class MinAgeAttribute : ValidationAttribute
    {

        private readonly int _minAge;

        public MinAgeAttribute(int minAge)
        {
            _minAge = minAge;
            ErrorMessage = $"You must be at least {_minAge} years old to create an account.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is int age))
            {
                return new ValidationResult("Age is required.");
            }

            if (age >= _minAge)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
