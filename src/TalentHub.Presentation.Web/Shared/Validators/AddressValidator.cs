using FluentValidation;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.Presentation.Web.Shared.Validators;

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(p => p.Street)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Number)
            .NotNull()
            .NotEmpty()
            .Matches(@"^\d+$");

        RuleFor(p => p.Neighborhood)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.City)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.State)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Country)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.ZipCode)
            .NotNull()
            .NotEmpty()
            .Matches(@"^\d+$")
            .MinimumLength(8)
            .MaximumLength(8);
    }
}
