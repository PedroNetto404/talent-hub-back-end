using System.Net.Mime;
using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Certificates.UpdateAttachment;

public class UpdateCandidateCertificateAttachmentRequestValidator : AbstractValidator<UpdateCandidateCertificateAttachmentRequest>
{
    public UpdateCandidateCertificateAttachmentRequestValidator()
    {
        RuleFor(p => p.File)
            .NotNull()
            .NotEmpty()
            .Custom((file, context) =>
            {
                if (file.Length == 0)
                {
                    context.AddFailure("File", "File is empty");
                }

                if (file.ContentType != MediaTypeNames.Application.Pdf)
                {
                    context.AddFailure("File", "File must be a PDF");
                }
            });
    }
}
