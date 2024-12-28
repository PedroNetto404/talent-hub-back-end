using FluentValidation;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.Presentation.Web.Shared.Validators;

namespace TalentHub.Presentation.Web.Endpoints.Companies.This.Update;

public sealed class UpdateCompanyRequestValidator :
    AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyRequestValidator()
    {
        RuleFor(p => p.LegalName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);    

        RuleFor(p => p.TradeName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Cnpj)
            .NotEmpty()
            .NotNull()
            .Matches(@"^\d{14}$")
            .WithMessage("CNPJ must have 14 digits")
            .Custom((cnpj, ctx) =>
            {
                if(!Cnpj.IsValid(cnpj))
                {
                    ctx.AddFailure("Invalid CNPJ");
                }
            });

        RuleFor(p => p.About)   
            .NotNull()
            .NotEmpty()
            .MaximumLength(500)
            .When(p => p.About is not null);

        RuleFor(p => p.SectorId)
            .NotEmpty()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.RecruitmentEmail)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(p => p.Phone)
            .NotEmpty()
            .NotNull()
            .Matches(@"^\d{10,11}$")
            .WithMessage("Phone must have 10 or 11 digits")
            .When(p => p.Phone is not null);

        RuleFor(p => p.EmployeeCount)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.SiteUrl)
            .NotNull()
            .NotEmpty()
            .Matches(@"^https?://.*$");

        RuleFor(p => p.Address)
            .NotNull()
            .SetValidator(new AddressValidator());

        RuleFor(p => p.InstagramUrl)
            .NotNull()
            .NotEmpty()
            .Matches(@"^https?://.*$");

        RuleFor(p => p.LinkedinUrl)
            .NotNull()
            .NotEmpty()
            .Matches(@"^https?://.*$");

        RuleFor(p => p.CareerPageUrl)
            .NotNull()
            .NotEmpty()
            .Matches(@"^https?://.*$");

        RuleFor(p => p.Mission)
            .NotNull()
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(p => p.Vision)
            .NotNull()
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(p => p.Values)
            .NotNull()
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(p => p.FoundationYear)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(p => p.AutoMatchEnabled)
            .NotNull();

        RuleFor(p => p.CompanyId)   
            .NotEmpty()
            .NotEqual(Guid.Empty);  
    }
}
