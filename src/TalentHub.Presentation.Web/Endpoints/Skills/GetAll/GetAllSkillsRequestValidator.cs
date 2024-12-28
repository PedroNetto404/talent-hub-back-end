using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Skills.GetAll;

public sealed class GetAllSkillsRequestValidator : AbstractValidator<GetAllSkillsRequest>
{
    private static readonly string[] AllowedSortingFields = ["id", "type", "name"];

    public GetAllSkillsRequestValidator()
    {
        RuleForEach(p => p.Ids)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty)
            .When(p => p.Ids?.Any() ?? false);

        RuleFor(p => p.Type)
            .NotNull()
            .NotEmpty()
            .When(p => p.Type.HasValue);

        RuleFor(p => p.Limit)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .When(p => p.Limit.HasValue);

        RuleFor(p => p.Offset)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .When(p => p.Offset.HasValue);

        RuleFor(p => p.SortBy)
            .NotNull()
            .NotEmpty()
            .Custom((s, ctx) =>
            {
                if (!AllowedSortingFields.Contains(s))
                {
                    ctx.AddFailure("sort by", $"sort by must be one of {string.Join(", ", AllowedSortingFields)}");
                }
            })
            .When(p => !string.IsNullOrWhiteSpace(p.SortBy));
    }
}