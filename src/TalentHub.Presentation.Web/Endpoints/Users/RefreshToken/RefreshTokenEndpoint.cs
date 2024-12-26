using System.Net.Mime;
using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.RefreshToken;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Users.RefreshToken;

public class RefreshTokenEndpoint :
    Ep.Req<RefreshTokenRequest>
      .Res<AuthenticationResult>
{
    public override void Configure()
    {
        Post("{userId:guid}/refresh-token");
        Validator<RefreshTokenRequestValidator>();
        Description(ep =>
            ep.Accepts<RefreshTokenRequest>(MediaTypeNames.Application.Json)
              .Produces<AuthenticationResult>()
              .Produces(StatusCodes.Status400BadRequest)
        );
        AllowAnonymous();
        Version(1);
        Group<UsersEndpointsGroup>();
    }

    public override Task HandleAsync(RefreshTokenRequest req, CancellationToken ct) =>
        this.HandleUseCaseAsync(new RefreshTokenCommand(req.UserId, req.RefreshToken), ct);
}
