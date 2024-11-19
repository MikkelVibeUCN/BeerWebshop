using BeerWebshop.Web.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class RequiredIfCustomAddressAttribute : ValidationAttribute
{
    private readonly string _customAddressProperty;

    public RequiredIfCustomAddressAttribute(string customAddressProperty)
    {
        _customAddressProperty = customAddressProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var checkout = (Checkout)validationContext.ObjectInstance;

        // Check if WantsCustomAddress is true and value is null or empty
        if (checkout.WantsCustomAddress && string.IsNullOrWhiteSpace(value?.ToString()))
        {
            return new ValidationResult(ErrorMessage ?? "This field is required when using a custom address.");
        }

        return ValidationResult.Success;
    }
}
