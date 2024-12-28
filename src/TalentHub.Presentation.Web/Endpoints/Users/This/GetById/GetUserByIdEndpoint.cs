using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Queries.GetById;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Users.This.GetById;

public sealed class GetUserByIdEndpoint : Ep.Req<GetUserByIdRequest>.Res<UserDto>
{
    public override void Configure()
    {
        Get("{userId:guid}");

        Description(b =>
            b.Produces<UserDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest));

        Version(1);
        Group<UsersEndpointsGroup>();
    }

    public override Task HandleAsync(
        GetUserByIdRequest req,
        CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new GetUserByIdQuery(req.UserId),
            ct
        );
}
