using System.ComponentModel.DataAnnotations;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Attributes;

public sealed class AddressAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not Address address)
            return new ValidationResult("Invalid address type");

        var propValues =
            address.GetType()
                   .GetProperties()
                   .Select(p => (string)p.GetValue(address)!);

        if (propValues.Any(string.IsNullOrWhiteSpace))
            return new ValidationResult("Invalid address");

        return ValidationResult.Success;
    }
}