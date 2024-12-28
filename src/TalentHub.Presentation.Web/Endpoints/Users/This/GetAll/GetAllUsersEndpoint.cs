using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Queries.GetAll;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Users.This.GetAll;

public sealed class GetAllUsersEndpoint :
    Ep.Req<GetAllUsersRequest>.Res<PageResponse<UserDto>>
{
    public override void Configure()
    {
        Get("");
        Version(1);
        Group<UsersEndpointsGroup>();
        Validator<GetAllUsersRequestValidator>();
    }

    public override Task HandleAsync(GetAllUsersRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetAllUsersQuery(
                req.UsernameLike,
                req.EmailLike,
                req.Role,
                req.Limit ?? 10,
                req.Offset ?? 0,
                req.SortBy ?? "id",
                req.SortOrder ?? SortOrder.Ascending
            ),
            ct
        );
}
