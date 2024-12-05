using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Authenticate;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.Create;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.RefreshToken;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/users")]
public sealed class UserController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new CreateUserCommand(
            request.Email,
            request.Username,
            request.Password,
            request.Role
        ),
        cancellationToken: cancellationToken
    );

    [HttpPost("auth")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AuthenticateAsync(
      AuthenticateRequest request,
      CancellationToken cancellationToken)
    {
        if (request is { GrantType: "refresh_token", RefreshToken: not null })
        {
            return await HandleAsync(
                new RefreshTokenCommand(request.RefreshToken), 
                cancellationToken: cancellationToken
            );
        }

        if (request.GrantType == "client_credentials" &&
            !string.IsNullOrWhiteSpace(request.Password) &&
            (!string.IsNullOrWhiteSpace(request.Username) || !string.IsNullOrWhiteSpace(request.Email)))
        {
            return await HandleAsync(
                new AuthenticateUserCommand(
                    request.Username,
                    request.Email,
                    request.Password), 
                cancellationToken: cancellationToken);
        }

        return BadRequest(Error.BadRequest("Invalid authentication options"));
    }
}
