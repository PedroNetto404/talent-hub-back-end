using FluentValidation;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdatePresentationVideo;

public sealed class UpdateCompanyPresentationVideoCommandValidator : AbstractValidator<UpdateCompanyPresentationVideoCommand>
{
    public UpdateCompanyPresentationVideoCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.File).NotEmpty().Custom((file, context) =>
        {
            if (file.Length > Company.MaxPresentationVideoBytes)
            {
                context.AddFailure("File", $"File size must be less than {Company.MaxPresentationVideoBytes} bytes");
            }
        });
        RuleFor(x => x.FileContentType).NotEmpty();
    }
}
