using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Universities.Update;

public sealed class UpdateUniversityRequestValidator : AbstractValidator<UpdateUniversityRequest>
{
  public UpdateUniversityRequestValidator()
  {
    RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(200).NotNull();
    RuleFor(x => x.SiteUrl).NotEmpty().NotNull().MinimumLength(3).MaximumLength(200).Matches(@"^https?://");
  }
}
