using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Delete;

public sealed class DeleteCourseRequestValidator :
    AbstractValidator<DeleteCourseRequest>
{
  public DeleteCourseRequestValidator()
  {
    RuleFor(x => x.CourseId)
        .NotNull()
        .NotEmpty()
        .NotEqual(Guid.Empty);
  }
}
