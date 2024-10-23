using System.ComponentModel.DataAnnotations;

namespace TalentHub.Presentation.Web.Attributes;

public sealed class NotInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is DateOnly dateOnly &&
        dateOnly > DateOnly.FromDateTime(DateTime.Now) 
        ? new ValidationResult("Date cannot be in the future.")
        : ValidationResult.Success;
}