using FluentValidation;
using TalentHub.Presentation.Web.Shared.Validators;

namespace TalentHub.Presentation.Web.Endpoints.Universities.GetAll;

public sealed class GetAllUniversitiesRequestValidator :
    PageRequestValidator<GetAllUniversitiesRequest>
{
  private static readonly string[] AllowedSortingFields = [
      "id",
        "name"
  ];

  protected override string[] SortableFields => AllowedSortingFields;

  public GetAllUniversitiesRequestValidator()
  {
    RuleFor(p => p.NameLike)
        .NotNull()
        .NotEmpty()
        .When(p => !string.IsNullOrWhiteSpace(p.NameLike));
  }
}
