using FastEndpoints;
using FluentValidation;
using TalentHub.ApplicationCore.Resources.Courses.Dtos;
using TalentHub.ApplicationCore.Resources.Courses.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Courses.Create;

public sealed class CreateCourseRequestValidator :
    AbstractValidator<CreateCourseRequest>
{
    public CreateCourseRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleForEach(p => p.Tags)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleForEach(p => p.RelatedSkillIds)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
