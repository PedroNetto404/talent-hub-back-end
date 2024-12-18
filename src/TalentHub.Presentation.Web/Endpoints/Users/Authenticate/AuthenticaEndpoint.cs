using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;

namespace TalentHub.Presentation.Web.Endpoints.Users.Authenticate;

public sealed class AuthenticaEndpoint : 
    Ep.Req<AuthenticateRequest>
      .Res<AuthenticationResult>
{
    public override void Configure()
    {
        Post("auth");
        AllowAnonymous();

        Description(d =>
        {
            d.Produces(StatusCodes.Status200OK, typeof(AuthenticationResult));
            d.Produces(StatusCodes.Status400BadRequest);
            d.WithDescription("Authenticate a user.");
            d.WithDisplayName("Authenticate User");
        });

        Group<UsersEndpointsGroup>();
        Version(1);
    }

    public override async Task HandleAsync(AuthenticateRequest req, CancellationToken ct)
    {
        Result<AuthenticationResult> result = await Resolve<ISender>().Send(
            new AuthenticateUserCommand(
                req.Email,
                req.Username,
                req.Password),
            ct
        );

        if (result is { IsFail: true, Error: var error})
        {
            await SendResultAsync(Results.BadRequest(error));
        }

        await SendOkAsync(result.Value, cancellation: ct);
    }
}
