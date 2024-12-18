using System.Net.Mime;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Users.Create;

public sealed class CreateUserEndpoint : Ep.Req<CreateUserRequest>.Res<UserDto>
{
    public override void Configure()
    {
        Post("");
        AllowAnonymous();

        Description(d => 
            d.Produces(StatusCodes.Status201Created, typeof(UserDto))
             .Produces(StatusCodes.Status400BadRequest)
             .Accepts<CreateUserRequest>(MediaTypeNames.Application.Json)
             .WithDescription("Create a new user.")
             .WithDisplayName("Create User")
        );

        Group<UsersEndpointsGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct) =>
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
                    new CreateUserCommand(
                        req.Email,
                        req.Username,
                        req.Password,
                        req.Role
                    ),
                    ct
                ),
                (dto) => Results.Created($"/api/v1/users/{dto.Id}", dto)
        ));
}

