using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Users.SubResouces.Authenticate;

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
        });
        Validator<AuthenticateRequestValidator>();
        Version(1);
        Group<UsersEndpointsGroup>();
    }

    public override Task HandleAsync(AuthenticateRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(new AuthenticateUserCommand(
                req.Email,
                req.Username,
                req.Password
            ),
            ct);
}
