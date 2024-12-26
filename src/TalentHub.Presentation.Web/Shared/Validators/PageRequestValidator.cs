using FluentValidation;
using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Shared.Validators;

public class PageRequestValidator : PageRequestValidator<PageRequest>
{
    protected override string[] SortableFields => ["id"];
}

public abstract class PageRequestValidator<T>
    : AbstractValidator<T> where T : PageRequest
{
    protected abstract string[] SortableFields { get; }

    public PageRequestValidator()
    {
        RuleFor(p => p.Limit)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .When(p => p.Limit.HasValue);

        RuleFor(p => p.Offset)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .When(p => p.Offset.HasValue);

        RuleFor(p => p.SortBy)
            .NotEmpty()
            .NotNull()
            .Must(p => SortableFields.Contains(p))
            .When(p => !string.IsNullOrWhiteSpace(p.SortBy));

        RuleFor(p => p.SortOrder)
            .NotEmpty()
            .NotNull()
            .When(p => !string.IsNullOrWhiteSpace(p.SortBy));
    }
}
