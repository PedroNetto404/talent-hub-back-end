using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Create;

public sealed class CreateCompanySectorCommandValidator : AbstractValidator<CreateCompanySectorCommand>
{
    public CreateCompanySectorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Company sector name is required")
            .MaximumLength(100)
            .WithMessage("Company sector name must not exceed 100 characters");
    }
}
