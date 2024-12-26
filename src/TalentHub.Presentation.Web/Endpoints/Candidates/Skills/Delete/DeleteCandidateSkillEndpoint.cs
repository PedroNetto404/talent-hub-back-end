using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Delete;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Skills.Delete;

public sealed class DeleteCandidateSkillEndpoint :
    Ep.Req<DeleteCandidateSkillRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Delete("{candidateSkillId:guid}");
        Version(1);
        Group<CandidateSkillEndpointSubGroup>();
        Validator<DeleteCandidateSkillRequestValidator>();
    }

    public override Task HandleAsync(
        DeleteCandidateSkillRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new DeleteCandidateSkillCommand(
            req.CandidateId,
            req.CandidateSkillId
        ),
        ct);
}
