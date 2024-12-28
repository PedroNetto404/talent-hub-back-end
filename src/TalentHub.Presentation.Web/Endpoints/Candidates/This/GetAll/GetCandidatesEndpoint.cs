using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetAllCandidates;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Candidates.This.GetAll;

public sealed class GetCandidatesEndpoint :
    Ep.Req<GetCandidatesRequest>
    .Res<PageResponse<CandidateDto>>
{
    public override void Configure()
    {
        Get("");
        Validator<GetCandidatesRequestValidator>();
        Version(1);
        Group<CandidatesEndpointsGroup>();
        RequestBinder(new GetCandidatesRequestBinder());

        Description(b =>
            b.Accepts<GetCandidatesRequest>()
                .Produces<PageResponse<CandidateDto>>()
                .Produces(StatusCodes.Status400BadRequest)
        );

        Permissions(Permission.ReadAllCandidates);
    }

    public override Task HandleAsync(GetCandidatesRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetAllCandidatesQuery(
                req.SkillIds!,
                req.Languages!,
                req.Limit!.Value,
                req.Offset!.Value,
                req.SortBy,
                req.SortOrder!.Value
            ),
            ct);
}
