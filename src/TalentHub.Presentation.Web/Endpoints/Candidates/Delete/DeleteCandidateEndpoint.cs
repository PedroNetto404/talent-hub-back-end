using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Delete;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.Delete;

public sealed class DeleteCandidateEndpoint
    : Ep.Req<DeleteCandidateRequest>.NoRes
{
    public override void Configure()
    {
        Delete("{candidateId:guid}");
        Version(1);
        Group<CandidatesEndpointsGroup>();
        Validator<DeleteCandidateRequestValidator>();
    }

    public override async Task HandleAsync(
        DeleteCandidateRequest req,
        CancellationToken ct
    ) => await SendResultAsync(
        ResultUtils.Map(
            await Resolve<ISender>().Send(
                new DeleteCandidateCommand(
                    req.CandidateId
                ),
                ct
            )
        )
    );
}
