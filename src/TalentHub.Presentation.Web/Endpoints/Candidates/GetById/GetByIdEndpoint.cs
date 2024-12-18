using System.Net.Mime;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetCandidateById;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetById;

public sealed class GetByIdEndpoint :
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
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid candidateId = Route<Guid>("candidateId");
        Result<CandidateDto> result = await Resolve<ISender>().Send<Result<CandidateDto>>(
            new GetCandidateByIdQuery(candidateId),
            ct
        );

        if(result is { IsFail: true, Error: var error})
        {
            await SendResultAsync(Results.BadRequest(error));
        }

        await SendOkAsync(result.Value, cancellation: ct);
    }
}
