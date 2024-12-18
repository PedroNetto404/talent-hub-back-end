using System.Net.Mime;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.RefreshToken;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Users.RefreshToken;

public class RefreshTokenEndpoint :
    Ep.Req<RefreshTokenRequest>
      .Res<AuthenticationResult>
{
    public override void Configure()
    {
        Post("{userId:guid}/refresh-token");
        Validator<RefreshTokenCommandValidator>();

        Description(ep =>
            ep.Accepts<RefreshTokenRequest>(MediaTypeNames.Application.Json)
              .Produces<AuthenticationResult>()
              .Produces(StatusCodes.Status400BadRequest));

        Group<UsersEndpointsGroup>();
        Version(1);
    }

    public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
    {
        Guid userId = Route<Guid>("userId");

        await SendResultAsync(
            ResultUtils.MatchResult(
                await Resolve<ISender>().Send(
                    new RefreshTokenCommand(
                        userId,
                        req.RefreshToken
                    ),
                    ct
                )
            )
        );
    }
}
