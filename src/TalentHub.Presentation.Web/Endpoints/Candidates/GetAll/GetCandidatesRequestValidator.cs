using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed class GetCandidatesRequestValidator : AbstractValidator<GetCandidatesRequest>
{
    private static readonly string[] AllowedSortingFields = [
        "id",
        "name",
        "phone"
    ];

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

        RuleFor(p => p.SortBy)
            .Custom((sortBy, context) =>
            {
                if (!AllowedSortingFields.Contains(sortBy))
                {
                    context.AddFailure("_sort_by", $"_sort_by must be one of {string.Join(", ", AllowedSortingFields)}");
                }
            })
            .When(p => !string.IsNullOrWhiteSpace(p.SortBy));

        RuleFor(p => p.SortOrder)
            .NotNull()
            .When(p => !string.IsNullOrWhiteSpace(p.SortBy));
    }
}
