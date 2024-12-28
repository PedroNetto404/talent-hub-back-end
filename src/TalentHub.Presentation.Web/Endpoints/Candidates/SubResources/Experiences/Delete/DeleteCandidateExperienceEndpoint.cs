using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.DeleteExperience;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Experiences.Delete;

public sealed class DeleteCandidateExperienceEndpoint :
    Ep.Req<DeleteCandidateExperienceRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{experienceId:guid}");
        Group<ExperiencesSubGroup>();
        Validator<DeleteCandidateExperienceRequestValidator>();
        Version(1);
    }

    public override Task HandleAsync(
        DeleteCandidateExperienceRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new DeleteExperienceCommand(req.CandidateId, req.ExperienceId),
        ct
    );
}
