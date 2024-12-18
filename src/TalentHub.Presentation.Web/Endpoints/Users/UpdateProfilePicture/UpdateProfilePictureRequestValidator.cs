using System.Net.Mime;
using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Users.UpdateProfilePicture;

public sealed class UpdateProfilePictureRequestValidator : AbstractValidator<UpdateProfilePictureRequest>
{
    public UpdateProfilePictureRequestValidator()
    {
        RuleFor(p => p.File)
        .NotEmpty()
        .NotNull()
        .Custom((file, context) =>
        {
            if (file is { Length: > 0 })
            {
                context.AddFailure("Image is required");
            }

            if (file.ContentType is not (MediaTypeNames.Image.Jpeg or MediaTypeNames.Image.Png))
            {
                context.AddFailure("Image content type must be image/png or image/jpeg");
            }
        });
    }
}
