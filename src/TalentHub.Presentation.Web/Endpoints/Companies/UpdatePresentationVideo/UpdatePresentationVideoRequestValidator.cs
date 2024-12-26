using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdatePresentationVideo;

public sealed class UpdatePresentationVideoRequestValidator :
    AbstractValidator<UpdatePresentationVideoRequest>
{
  public UpdatePresentationVideoRequestValidator()
  {
    RuleFor(x => x.CompanyId).NotEmpty().NotEqual(Guid.Empty).NotNull();
    RuleFor(x => x.File)
    .NotEmpty()
    .NotNull()
    .Custom((file, context) =>
    {
      if (file.ContentType != "video/mp4" && file.ContentType != "video/webm")
      {
        context.AddFailure("File", "Only video files are allowed.");
      }
    });
  }
}
