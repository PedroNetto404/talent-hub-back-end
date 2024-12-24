using System.Net.Mime;
using FluentValidation;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.UpdateResume;

public sealed class UpdateResumeRequestValidator : AbstractValidator<UpdateResumeRequest>
{
    public UpdateResumeRequestValidator()
    {
        RuleFor(p => p.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.File)
            .NotEmpty()
            .NotNull()
            .Custom((file, ctx) =>
            {
                if (file.ContentType is not MediaTypeNames.Application.Pdf)
                {
                    ctx.AddFailure("File", "file content type must be a application/pdf");
                }

                if(file.Length == 0)
                {
                    ctx.AddFailure("File", "file content is empty");
                }
            });
    }
}
