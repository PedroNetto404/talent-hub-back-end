using FastEndpoints;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetAll;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Universities.GetAll;

public sealed class GetAllUniversitiesEndpoint :
    Ep.Req<GetAllUniversitiesRequest>.Res<PageResponse<UniversityDto>>
{
    public override void Configure()
    {
        Get("");
        Group<UniversitiesGroup>();
        Validator<GetAllUniversitiesRequestValidator>();
        Version(1);
        Description(b =>
            b.Accepts<GetAllUniversitiesRequest>()
                .Produces<PageResponse<UniversityDto>>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError)
        );

        Definition.AllowedRoles?.Clear();
    }

    public override Task HandleAsync(GetAllUniversitiesRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetAllUniversitiesQuery(
                req.NameLike,
                 req.Limit ?? 10,
                  req.Offset ?? 0,
                   req.SortBy,
                    req.SortOrder ?? ApplicationCore.Shared.Enums.SortOrder.Ascending
                ),
                ct
        );
}
