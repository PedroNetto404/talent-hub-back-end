using FluentValidation;
using TalentHub.Presentation.Web.Shared.Validators;

namespace TalentHub.Presentation.Web.Endpoints.Courses.GetAll;

public sealed class GetAllCoursesRequestValidator : PageRequestValidator<GetAllCoursesRequest>
{
  private static readonly string[] AllowedSortingFields = [
      "id",
        "name"
  ];

  public GetAllCoursesRequestValidator()
  {
    RuleFor(x => x.NameLike)
        .NotNull()
        .NotEmpty()
        .When(x => !string.IsNullOrWhiteSpace(x.NameLike));

    RuleForEach(x => x.RelatedSkillIds)
        .NotEmpty()
        .NotNull()
        .When(x => x.RelatedSkillIds is not null);
  }

  protected override string[] SortableFields => AllowedSortingFields;
}
