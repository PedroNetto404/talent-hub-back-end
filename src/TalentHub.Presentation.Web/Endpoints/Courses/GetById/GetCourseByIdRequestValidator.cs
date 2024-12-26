using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Courses.GetById;

public sealed class GetCourseByIdRequestValidator :
    AbstractValidator<GetCourseByIdRequest>
{
    public GetCourseByIdRequestValidator()
    {
        RuleFor(p => p.CourseId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
