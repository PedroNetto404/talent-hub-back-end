using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Certificates.Create;

public sealed class CreateCertificateRequestValidator : AbstractValidator<CreateCertificateRequest>
{
    public CreateCertificateRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Issuer)
           .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Workload)
            .GreaterThan(0);

        RuleForEach(p => p.RelatedSkills)
            .NotEqual(Guid.Empty);
    }
}

