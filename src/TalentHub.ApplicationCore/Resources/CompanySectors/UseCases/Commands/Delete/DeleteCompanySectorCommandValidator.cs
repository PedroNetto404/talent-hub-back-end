using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Delete;

public sealed class DeleteCompanySectorCommandValidator : AbstractValidator<DeleteCompanySectorCommand>
{
    public DeleteCompanySectorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .NotEmpty();
    }
}
