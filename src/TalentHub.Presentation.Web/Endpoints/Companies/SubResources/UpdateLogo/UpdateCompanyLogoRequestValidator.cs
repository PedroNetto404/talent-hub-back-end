using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.SubResources.UpdateLogo;

public sealed class UpdateCompanyLogoRequestValidator :
    AbstractValidator<UpdateCompanyLogoRequest>
{
    public UpdateCompanyLogoRequestValidator()
    {
        RuleFor(p => p.CompanyId)
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.File)
            .NotNull()
            .NotEmpty()
            .Custom((file, ctx) =>
            {
                if(file.Length == 0)
                {
                    ctx.AddFailure("File is empty");
                }

                if(file.Length > 5 * 1024 * 1024)
                {
                    ctx.AddFailure("File is too large");
                }

                if(file.ContentType is not "image/jpeg" and not "image/png")
                {
                    ctx.AddFailure("File must be a JPEG or PNG image");
                }
            });
    }
}
