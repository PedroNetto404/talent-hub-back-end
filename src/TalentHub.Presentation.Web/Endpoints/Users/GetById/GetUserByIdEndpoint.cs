using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Queries.GetById;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Users.GetById;

public sealed class GetUserByIdEndpoint : Ep.NoReq.Res<UserDto>
{
    public override void Configure()
    {
        Get("{userId:guid}");

        Description(b => 
            b.Produces<UserDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithDisplayName("Get User By Id")
            .WithDescription("Get a user by their id."));

        Version(1);
        Group<UsersEndpointsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Guid userId = Route<Guid>("userId");

        GetUserByIdQuery query = new(userId);
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send<Result<UserDto>>(query, ct)
            )
        );
    }
}
