using MediatR;
using Microsoft.AspNetCore.Mvc;
using TalentHub.ApplicationCore.Users.UseCases.Commands.Authenticate;
using TalentHub.ApplicationCore.Users.UseCases.Commands.Create;
using TalentHub.ApplicationCore.Users.UseCases.Commands.RefreshToken;
using TalentHub.Presentation.Web.Models.Request;

namespace TalentHub.Presentation.Web.Controllers;

[Route("api/users")]
public sealed class UserController(ISender sender) : ApiController(sender)
{
    [HttpPost]
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

    [HttpPost("authentication/token")]
    public Task<IActionResult> AuthenticateAsync(
        AuthenticateUserRequest request,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new AuthenticateUserCommand(
            request.Email,
            request.Username,
            request.Password
        ),
        cancellationToken: cancellationToken
    );

    [HttpPost("authentication/refresh_token")]
    public Task<IActionResult> RefreshTokenAsync(
        [FromHeader(Name = "Authorization")] string refreshToken,
        CancellationToken cancellationToken
    ) => HandleAsync(
        new RefreshTokenCommand(
            refreshToken
        ),
        cancellationToken: cancellationToken
    );
}
