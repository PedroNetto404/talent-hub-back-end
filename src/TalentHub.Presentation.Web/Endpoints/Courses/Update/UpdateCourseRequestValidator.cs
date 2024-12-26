using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Update;

public sealed class UpdateCourseRequestValidator : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseRequestValidator()
    {
        RuleFor(p => p.CourseId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleForEach(p => p.Tags)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(maximumLength: 200);

        RuleForEach(p => p.RelatedSkillIds)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
