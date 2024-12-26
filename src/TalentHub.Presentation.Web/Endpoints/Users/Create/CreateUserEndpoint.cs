using System.Net.Mime;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Create;
using TalentHub.Presentation.Web.Extensions;

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
        );
        Validator<CreateUserRequestValidator>();
        Group<UsersEndpointsGroup>();
        Version(1);
    }

    public override Task HandleAsync(CreateUserRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(
            new CreateUserCommand(
                req.Email,
                req.Username,
                req.Password,
                req.Role
            ),
            ct,
            onSuccessCallback: (dto) => Results.Created($"/api/v1/users/{dto.Id}", dto));
}
