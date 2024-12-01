using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.Infra.Security.Services;

public sealed class HttpUserContext(
    IHttpContextAccessor httpContextAccessor,
    IRepository<User> userRepository
) : IUserContext
{
    public async Task<Result<User>> GetCurrentAsync()
    {
        HttpContext context = httpContextAccessor.HttpContext!;

        if (!context.User.Identity!.IsAuthenticated)
        {
            return new Error("user", "user not authenticated");
        }

        Claim? userIdClaim = context.User.Claims.FirstOrDefault(
            c => c.Type == ClaimTypes.NameIdentifier
        );
        if (
            userIdClaim is null
            || string.IsNullOrWhiteSpace(userIdClaim.Value)
            || !Guid.TryParse(userIdClaim.Value, out Guid userId)
        )
        {
            return new Error("user", "invalid user id");
        }

        User? user = await userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return Error.NotFound("user");
        }

        return user;
    }
}
