using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.GetAll;

public sealed class GetAllCompaniesRequestValidator :
    AbstractValidator<GetAllCompaniesRequest>
{
  private static readonly string[] ValidSortByFields =
  [
      "id",
        "name",
        "legal_name",
        "trade_name",
        "cnpj",
        "phone",
        "foundation_year",
        "recruitment_email",
    ];

  public GetAllCompaniesRequestValidator()
  {
    RuleFor(p => p.NameLike)
        .MaximumLength(100)
        .When(p => p.NameLike is not null);

    RuleForEach(p => p.SectorIds)
        .NotEmpty()
        .NotEqual(Guid.Empty)
        .When(p => p.SectorIds?.Any() is true);

    RuleFor(p => p.LocationLike)
        .MaximumLength(100)
        .When(p => p.LocationLike is not null);

    RuleFor(p => p.Limit)
        .NotEmpty()
        .GreaterThan(0)
        .When(p => p.Limit is not null);

    RuleFor(p => p.Offset)
        .NotEmpty()
        .GreaterThanOrEqualTo(0)
        .When(p => p.Offset is not null);

    RuleFor(p => p.SortBy)
        .NotEmpty()
        .Custom((sortBy, context) =>
        {
          if (sortBy is not null && !ValidSortByFields.Contains(sortBy))
          {
            context.AddFailure($"Invalid sort by field. Valid fields are: {string.Join(", ", ValidSortByFields)}");
          }
        })
        .When(p => p.SortBy is not null);

    RuleFor(p => p.SortOrder)
        .NotEmpty()
        .NotNull()
        .When(p => p.SortOrder is not null);
  }
}
