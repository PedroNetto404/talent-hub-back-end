using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Update;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Skills.Update;

public sealed class UpdateCandidateSkillEndpoint :
    Ep.Req<UpdateCandidateSkillRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Patch("{candidateSkillId:guid}");
        Group<CandidateSkillEndpointSubGroup>();
        Version(1);
        Validator<UpdateCandidateSkillRequestValidator>();
    }

    public override Task HandleAsync(
        UpdateCandidateSkillRequest req,
        CancellationToken ct
    ) => this.HandleUseCaseAsync(
        new UpdateCandidateSkillProficiencyCommand(
            req.CandidateId,
            req.CandidateSkillId,
            req.Proficiency
        ),
        ct);
}
