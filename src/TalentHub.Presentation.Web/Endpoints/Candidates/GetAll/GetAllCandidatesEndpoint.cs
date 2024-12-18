using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.GetAll;

public sealed class GetAllCandidatesEndpoint : Ep.Req<GetCandidatesRequest>.Res<PageResponse>
{
    public override void Configure()
    {
        Get("");
        Group<CandidatesEndpointsGroup>();
        Validator<GetCandidatesRequestValidator>();
        Version(1);
    }

    public override async Task HandleAsync(GetCandidatesRequest req, CancellationToken ct) =>
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send<Result<PageResponse>>(
                    new GetAllCandidatesQuery(
                        req.SkillIds,
                        req.Languages,
                        req.Limit,
                        req.Offset,
                        req.SortBy,
                        req.Ascending
                    ),
                    ct
                )
            )
        );
}
