using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences.UseCases.Commands.Update;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.Experiences.Update;

public sealed class UpdateCandidateExperienceEndpoint
    : Ep.Req<UpdateCandidateExperienceRequest>
        .Res<CandidateDto>
{
    public override void Configure()
    {
        Put("{experienceId:guid}");
        Version(1);
        Group<ExperiencesSubGroup>();
        Validator<UpdateCandidateExperienceRequestValidator>();
    }

    public override Task HandleAsync(
        UpdateCandidateExperienceRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new UpdateExperienceCommand(
            req.CandidateId,
            req.ExperienceId,
            req.Type,
            req.Start,
            req.End,
            req.IsCurrent,
            req.Activities,
            req.AcademicEntities,
            req.CurrentSemester,
            req.Status,
            req.Description
        ),
        ct
    );
}
