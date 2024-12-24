using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Delete;

public sealed class DeleteLanguageProficiencyRequestValidator : AbstractValidator<DeleteLanguageProficiencyRequest>
{
    public DeleteLanguageProficiencyRequestValidator()
    {
        RuleFor(p => p.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.LanguageProficiencyId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
