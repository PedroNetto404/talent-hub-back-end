using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Certificates.Delete;

public sealed class DeleteCertificateRequestValidator : AbstractValidator<DeleteCertificateRequest>
{
    public DeleteCertificateRequestValidator()
    {
        RuleFor(p => p.CertificateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
