using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Companies.UpdateGaleryItem;

public sealed class UpdateCompanyGaleryItemRequestValidator :
    AbstractValidator<UpdateCompanyGaleryItemRequest>
{
    public UpdateCompanyGaleryItemRequestValidator()
    {
        RuleFor(p => p.CompanyId)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.File)
            .NotNull()
            .Empty()
            .Custom((file, ctx) =>
            {
                if(file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                {
                    ctx.AddFailure("File", "File must be a jpeg or png image");
                }

                if(file.Length >  5 * 1024 * 1024)
                {
                    ctx.AddFailure("File", "File must be less than 5MB");
                }
            });
    }
}
