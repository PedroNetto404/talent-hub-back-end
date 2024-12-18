using FluentValidation;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Update;

public sealed class UpdateCandidateRequestValidator : AbstractValidator<UpdateCandidateRequest>
{
    public UpdateCandidateRequestValidator()
    {
          RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        When(p => p.Summary != null, () => 
            RuleFor(p => p.Summary)
                .MinimumLength(3)
                .MaximumLength(500)
        );

        RuleFor(p => p.Phone)
            .NotEmpty()
            .NotNull()
            .MinimumLength(11)
            .MaximumLength(11);

        When(p => p.ExpectedRemuneration != null, () =>
            RuleFor(p => p.ExpectedRemuneration)
                .GreaterThan(0)
        );

        When(p => p.InstagramUrl != null, () =>
            RuleFor(p => p.InstagramUrl)
                .Custom((url, context) =>
                {
                    if(Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                    {
                        context.AddFailure("InstagramUrl", "Invalid Instagram URL");
                    }
                })
                .MinimumLength(3)
                .MaximumLength(100)
        );

        When(p => p.LinkedInUrl != null, () =>
            RuleFor(p => p.LinkedInUrl)
                .Custom((url, context) =>
                {
                    if(Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                    {
                        context.AddFailure("LinkedInUrl", "Invalid LinkedIn URL");
                    }
                })
                .MinimumLength(3)
                .MaximumLength(100)
        );

        When(p => p.GitHubUrl != null, () =>
            RuleFor(p => p.GitHubUrl)
                .Custom((url, context) =>
                {
                    if(Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                    {
                        context.AddFailure("GitHubUrl", "Invalid GitHub URL");
                    }
                })
                .MinimumLength(3)
                .MaximumLength(100)
        );

        When(p => p.WebsiteUrl != null, () =>
            RuleFor(p => p.WebsiteUrl)
                .Custom((url, context) =>
                {
                    if(Uri.TryCreate(url, UriKind.Absolute, out Uri? uri) == false)
                    {
                        context.AddFailure("WebsiteUrl", "Invalid Website URL");
                    }
                })
                .MinimumLength(3)
                .MaximumLength(100)
        );

        RuleFor(p => p.AddressStreet)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.AddressNumber)
            .NotEmpty()
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(15);

        RuleFor(p => p.AddressNeighborhood)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.AddressCity) 
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(p => p.AddressState)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(30);

        RuleFor(p => p.AddressCountry)
            .NotEmpty()
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(30);

        RuleFor(p => p.AddressZipCode)
            .NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .MaximumLength(8);

        When(p => p.Hobbies.Any(), () =>
            RuleForEach(p => p.Hobbies)
                .MinimumLength(3)
                .MaximumLength(100)
        );

        When(p => p.DesiredWorkplaceTypes.Any(), () =>
            RuleForEach(p => p.DesiredWorkplaceTypes)
                .Custom((type, context) =>
                {
                    if(Enum.TryParse<WorkplaceType>(type.Pascalize(), out _) == false)
                    {
                        context.AddFailure("DesiredWorkplaceTypes", "Invalid workplace type");
                    }
                })
        );

        When(p => p.DesiredJobTypes.Any(), () =>
            RuleForEach(p => p.DesiredJobTypes)
                .Custom((type, context) =>
                {
                    if(Enum.TryParse<JobType>(type.Pascalize(), out _) == false)
                    {
                        context.AddFailure("DesiredJobTypes", "Invalid job type");
                    }
                })
        );
    }
}
