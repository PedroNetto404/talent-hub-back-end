using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Update;

public sealed class UpdateCompanySectorCommandValidator : AbstractValidator<UpdateCompanySectorCommand>
{
    public UpdateCompanySectorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
