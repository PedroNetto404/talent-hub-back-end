using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Users.Authenticate;

public sealed class AuthenticateEndpoint : 
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
        Validator<AuthenticateRequestValidator>();
        Version(1);
        Group<UsersEndpointsGroup>();
    }

    public override async Task HandleAsync(AuthenticateRequest req, CancellationToken ct) => 
        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
                    new AuthenticateUserCommand(
                        req.Email,
                        req.Username,
                        req.Password
                    ),
                    ct
                )
            )
        );
}
