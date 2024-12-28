using FluentValidation;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.Presentation.Web.Shared.Validators;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.GetAll;

public sealed class GetCandidatesRequestValidator : PageRequestValidator<GetCandidatesRequest>
{
    private static readonly string[] AllowedSortingFields = [
        "id",
        "name",
        "phone"
    ];

    protected override string[] SortableFields => AllowedSortingFields;

    public GetCandidatesRequestValidator()
    {
        RuleForEach(q => q.SkillIds)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .When(p => p.SkillIds?.Any() ?? false);

        RuleForEach(p => p.Languages)
            .NotNull()
            .NotEmpty()
            .Custom((language, context) =>
            {
                if (!Language.List.Any(p => p.Name == language))
                {
                    context.AddFailure("Language", $"Language must be one of: {string.Join(", ", Language.List.Select(p => p.Name))}");
                }
            })
            .When(p => p.Languages?.Any() ?? false);
    }
}
