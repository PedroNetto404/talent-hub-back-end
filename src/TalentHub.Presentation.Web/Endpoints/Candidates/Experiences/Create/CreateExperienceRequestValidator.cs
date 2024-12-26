using System.Data;
using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Experiences.Create;

public sealed class CreateExperienceRequestValidator : AbstractValidator<CreateExperienceRequest>
{
    public CreateExperienceRequestValidator()
    {
        RuleFor(p => p.CandidateId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.Type)
            .NotEmpty()
            .NotNull()
            .Custom((t, ctx) =>
            {
                if (t != "acamemic" && t != "professional")
                {
                    ctx.AddFailure("Type", "experience type must be 'academic' or 'professional'. Check route value");
                }
            });

        RuleForEach(p => p.Activities)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(300)
            .When(p => p.Activities?.Any() ?? false);

        RuleFor(p => p.EducationalLevel)
            .NotNull()
            .NotEmpty()
            .Custom((l, ctx) =>
            {
                if (!Enum.TryParse<EducationLevel>(l.Pascalize(), true, out _))
                {
                    ctx.AddFailure("Educational Level", $"educational level mus be one of: {Enum.GetValues<EducationLevel>().Select(p => p.ToString().Underscore())}");
                }
            })
            .When(p => p.Type is "academic");

        RuleFor(p => p.ProgressStatus)
            .NotNull()
            .NotEmpty()
            .Custom((s, ctx) =>
            {
                if (!Enum.TryParse<ProgressStatus>(s.Pascalize(), true, out _))
                {
                    ctx.AddFailure("progress status", $"progress status must be one of: {Enum.GetValues<ProgressStatus>().Select(p => p.ToString().Underscore())}");
                }
            })
            .When(p => p.Type is "academic");

        RuleFor(p => p.CurrentSemester)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .When(p => p.Type == "academic");

        RuleForEach(p => p.AcademicEntities)
            .NotEmpty()
            .NotEmpty()
            .Custom((a, ctx) =>
            {
                if (Enum.TryParse<AcademicEntity>(a.Pascalize(), true, out _))
                {
                    ctx.AddFailure("Academic Entity", $"Academic entity must be one of : {Enum.GetValues<AcademicEntity>().Select(p => p.ToString().Underscore())}");
                }
            })
            .When(p => p.Type is "academic" && (p.AcademicEntities?.Any() ?? false));

        RuleFor(p => p.CourseId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .When(p => p.Type is "academic");

        RuleFor(p => p.UniversityId)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty)
            .When(p => p.Type is "academic");

        RuleFor(p => p.Position)
        .NotEmpty()
        .NotNull()
        .MinimumLength(3)
        .MaximumLength(100)
        .When(p => p.Type is "professional");

        RuleFor(p => p.Company)
        .NotNull()
        .NotEmpty()
        .MinimumLength(3)
        .MaximumLength(100)
        .When(p => p.Type is "professional");

        RuleFor(p => p.ProfessionalLevel)
            .NotNull()
            .NotEmpty()
            .Custom((p, ctx) =>
            {
                if (Enum.TryParse<ProfessionalLevel>(p.Pascalize(), true, out _))
                {
                    ctx.AddFailure("ProfessionalLevel", $"professional level must be one of: {Enum.GetValues<ProfessionalLevel>().Select(p => p.ToString().Underscore())}");
                }
            })
            .When(p => p.Type is "professional");
    }
}
