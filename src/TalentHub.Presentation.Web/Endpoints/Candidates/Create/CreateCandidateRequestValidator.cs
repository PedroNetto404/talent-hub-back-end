using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.Presentation.Web.Shared.Validators;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Create;

public sealed class CreateCandidateRequestValidator : AbstractValidator<CreateCandidateRequest>
{
    public CreateCandidateRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.Summary)
            .MinimumLength(3)
            .MaximumLength(500)
            .When(p => p.Summary is not null);

        RuleFor(p => p.Phone)
            .NotEmpty()
            .NotNull()
            .MinimumLength(11)
            .MaximumLength(11);

        RuleFor(p => p.BirthDate)
            .NotEmpty()
            .NotNull()
            .GreaterThan(new DateOnly(1900, 1, 1));

        RuleFor(p => p.ExpectedRemuneration)
            .GreaterThan(0)
            .When(p => p.ExpectedRemuneration is not null);

        RuleFor(p => p.InstagramUrl)
            .Custom((url, context) =>
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                {
                    context.AddFailure("InstagramUrl", "Invalid Instagram URL");
                }
            })
            .MinimumLength(3)
            .MaximumLength(100)
            .When(p => p.InstagramUrl is not null);

        RuleFor(p => p.LinkedInUrl)
            .Custom((url, context) =>
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                {
                    context.AddFailure("LinkedInUrl", "Invalid LinkedIn URL");
                }
            })
            .MinimumLength(3)
            .MaximumLength(100)
            .When(p => p.LinkedInUrl is not null);

        RuleFor(p => p.GitHubUrl)
            .Custom((url, context) =>
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                {
                    context.AddFailure("GitHubUrl", "Invalid GitHub URL");
                }
            })
            .MinimumLength(3)
            .MaximumLength(100)
            .When(p => p.GitHubUrl is not null);

        RuleFor(p => p.WebsiteUrl)
            .Custom((url, context) =>
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                {
                    context.AddFailure("WebsiteUrl", "Invalid Website URL");
                }
            })
            .MinimumLength(3)
            .MaximumLength(100)
            .When(p => p.WebsiteUrl is not null);

        RuleFor(p => p.Address).SetValidator(new AddressValidator());

        RuleForEach(p => p.Hobbies)
            .MinimumLength(3)
            .MaximumLength(100)
            .When(p => p.Hobbies?.Any() ?? false);

        RuleForEach(p => p.DesiredWorkplaceTypes)
            .Custom((type, context) =>
            {
                if (Enum.TryParse<WorkplaceType>(type.Pascalize(), out _) == false)
                {
                    context.AddFailure("DesiredWorkplaceTypes", "Invalid workplace type");
                }
            }).When(p => p.DesiredWorkplaceTypes.Any());

        RuleForEach(p => p.DesiredJobTypes)
            .Custom((type, context) =>
            {
                if (Enum.TryParse<JobType>(type.Pascalize(), out _) == false)
                {
                    context.AddFailure("DesiredJobTypes", "Invalid job type");
                }
            }).When(p => p.DesiredJobTypes?.Any() ?? false);
    }
}
