using System.Net.Mime;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetCandidateById;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetById;

public sealed class GetCandidateByIdEndpoint :
    Ep.NoReq.Res<CandidateDto>
{
    public override void Configure()
    {
        Get("{candidateId:guid}");
        Group<CandidatesEndpointsGroup>();

        Description(ep => 
            ep.Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status404NotFound)
              .Produces(StatusCodes.Status200OK));

        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid candidateId = Route<Guid>("candidateId");
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send<Result<CandidateDto>>(
                    new GetCandidateByIdQuery(candidateId),
                    ct
                )
            )
        );
    }
}
