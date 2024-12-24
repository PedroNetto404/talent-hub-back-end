using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Skills.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Skills.Create;

public sealed class CreateCandidateSkillEndpoint :
    Ep.Req<CreateCandidateSkillRequest>.Res<CandidateDto>
{
    public override void Configure()
    {
        Post("");
        Group<CandidateSkillEndpointSubGroup>();
        Validator<CreateCandidateSkillRequestValidator>();
        Version(1);
        Description(b =>
            b.Accepts<CreateCandidateSkillRequest>()
            .Produces<CandidateDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
        );
    }

    public override async Task HandleAsync(
        CreateCandidateSkillRequest req,
        CancellationToken ct
    ) => await SendResultAsync(
        ResultUtils.Map(
            await Resolve<ISender>().Send( 
                new CreateCandidateSkillCommand(
                    req.CandidateId,
                    req.SkillId,
                    req.Proficiency
                ),
                ct
            )
        )
    );
}
