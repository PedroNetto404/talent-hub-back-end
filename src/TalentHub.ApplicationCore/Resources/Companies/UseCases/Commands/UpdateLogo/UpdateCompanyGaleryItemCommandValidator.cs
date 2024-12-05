using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateLogo;

public sealed class UpdateCompanyLogoCommandValidator : AbstractValidator<UpdateCompanyLogoCommand>
{
    public UpdateCompanyLogoCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .NotEqual(Guid.Empty)
            .NotEmpty();

        RuleFor(x => x.File)
            .Custom((file, context) =>
            {
                if (file.Length > Company.MaxLogoBytes)
                {
                    context.AddFailure($"File size must be less than {Company.MaxLogoBytes} bytes");
                }
            })
            .NotEmpty();

        RuleFor(x => x.FileContentType)
            .NotEmpty();
    }
}
