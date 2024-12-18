using FluentValidation;
using TalentHub.ApplicationCore.Shared.Validators;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.LegalName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.TradeName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .MaximumLength(14);

        RuleFor(x => x.About)
            .MaximumLength(2000);

        RuleFor(x => x.SectorId)
            .NotEmpty();

        RuleFor(x => x.RecruitmentEmail)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Phone)
            .MaximumLength(20);

        RuleFor(x => x.EmployeeCount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.SiteUrl)
            .MaximumLength(200);

        RuleFor(x => x.Address)
            .NotNull()
            .SetValidator(new AddressValidator());

        RuleFor(x => x.InstagramUrl)
            .MaximumLength(200);

        RuleFor(x => x.LinkedinUrl)
            .MaximumLength(200);

        RuleFor(x => x.CareerPageUrl)
            .MaximumLength(200);

        RuleFor(x => x.Mission)
            .MaximumLength(2000);

        RuleFor(x => x.Vision)
            .MaximumLength(2000);

        RuleFor(x => x.Values)
            .MaximumLength(2000);

        RuleFor(x => x.FoundationYear)
            .GreaterThanOrEqualTo(0);
    }
}
