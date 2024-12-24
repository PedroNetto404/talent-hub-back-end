using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Update;
using TalentHub.Presentation.Web.Utils;

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

    public override async Task HandleAsync(
        UpdateCandidateSkillRequest req,
        CancellationToken ct
    ) => await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
                    new UpdateCandidateSkillProficiencyCommand(
                        req.CandidateId,
                        req.CandidateSkillId,
                        req.Proficiency
                    ),
                    ct
                )
            )
        );
}
