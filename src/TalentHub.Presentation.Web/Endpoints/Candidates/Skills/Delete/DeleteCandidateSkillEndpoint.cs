using System;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Delete;
using TalentHub.Presentation.Web.Utils;

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

    public override async Task HandleAsync(
        DeleteCandidateSkillRequest req,
        CancellationToken ct
    ) => await SendResultAsync(
        ResultUtils.Map(
            await Resolve<ISender>().Send(
                new DeleteCandidateSkillCommand(
                    req.CandidateId, 
                    req.CandidateSkillId
                ),
                ct
            )
        )
    );
}
