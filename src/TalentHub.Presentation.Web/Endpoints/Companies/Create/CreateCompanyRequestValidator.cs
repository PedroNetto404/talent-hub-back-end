using FluentValidation;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.Presentation.Web.Shared.Validators;

namespace TalentHub.Presentation.Web.Endpoints.Companies.Create;

public sealed class CreateCompanyRequestValidator :
    AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
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
            .NotNull()
            .GreaterThan(0);

        RuleFor(p => p.SiteUrl)
            .NotEmpty()
            .NotNull()
            .Custom((site, ctx) =>
            {
                if(Uri.TryCreate(site, UriKind.Absolute, out _) is false)
                {
                    ctx.AddFailure("Invalid site URL");
                }
            })
            .When(p => p.SiteUrl is not null);

        RuleFor(p => p.Address)
            .NotNull()
            .SetValidator(new AddressValidator());

        RuleFor(p => p.InstagramUrl)
            .NotEmpty()
            .NotNull()
            .Custom((instagram, ctx) =>
            {
                if(Uri.TryCreate(instagram, UriKind.Absolute, out _) is false)
                {
                    ctx.AddFailure("Invalid Instagram URL");
                }
            })
            .When(p => p.InstagramUrl is not null);

        RuleFor(p => p.LinkedinUrl)
            .NotEmpty()
            .NotNull()
            .Custom((linkedin, ctx) =>
            {
                if(Uri.TryCreate(linkedin, UriKind.Absolute, out _) is false)
                {
                    ctx.AddFailure("Invalid Linkedin URL");
                }
            })
            .When(p => p.LinkedinUrl is not null);

        RuleFor(p => p.CareerPageUrl)
            .NotEmpty()
            .NotNull()
            .Custom((career, ctx) =>
            {
                if(Uri.TryCreate(career, UriKind.Absolute, out _) is false)
                {
                    ctx.AddFailure("Invalid Career Page URL");
                }
            })
            .When(p => p.CareerPageUrl is not null);

        RuleFor(p => p.Mission)
            .NotEmpty()
            .NotNull()
            .MaximumLength(500)
            .When(p => p.Mission is not null);

        RuleFor(p => p.Vision)
            .NotEmpty()
            .NotNull()
            .MaximumLength(500)
            .When(p => p.Vision is not null);

        RuleFor(p => p.Values)
            .NotEmpty()
            .NotNull()
            .MaximumLength(500)
            .When(p => p.Values is not null);

        RuleFor(p => p.FoundationYear)
            .NotEmpty()
            .NotNull()
            .GreaterThan(1900);
    }
}
