using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Create;

public sealed class CreateUniversityRequestValidator :
    AbstractValidator<CreateUniversityRequest>
{
    public CreateUniversityRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(p => p.SiteUrl)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200)
            .Matches(@"^https?://");
    }
}
