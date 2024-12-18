using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed class GetAllEndpoint : Ep.Req<GetCandidatesRequest>.Res<PageResponse>
{
    public override void Configure()
    {
        Get("");
        Group<CandidatesEndpointsGroup>();
        Validator<GetCandidatesRequestValidator>();
        Version(1);
    }

    public override async Task HandleAsync(GetCandidatesRequest req, CancellationToken ct)
    {
        GetAllCandidatesQuery query = new(
            req.SkillIds,
            req.Languages,
            req.Limit,
            req.Offset,
            req.SortBy,
            req.Ascending
        );

        Result<PageResponse> result = await Resolve<ISender>()
            .Send<Result<PageResponse>>(query, ct);

        if (result is { IsFail: true, Error: var error })
        {
            await SendResultAsync(Results.BadRequest(error));
        }

        await SendOkAsync(result.Value, cancellation: ct);
    }
}
