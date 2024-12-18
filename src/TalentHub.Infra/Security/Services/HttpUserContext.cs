using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users;

namespace TalentHub.Infra.Security.Services;

public sealed class HttpUserContext(
    IHttpContextAccessor httpContextAccessor,
    IRepository<User> userRepository
) : IUserContext
{
    public Guid? UserId
    {
        get
        {
            ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
            if (user is null)
            {
                return null;
            }

            string? userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                return null;
            }

            return Guid.Parse(userId);
        }
    }

    public Task<User?> GetCurrentAsync(CancellationToken cancellationToken = default) =>
        UserId is null
        ? Task.FromResult<User?>(null)
        : userRepository.GetByIdAsync(UserId!.Value, cancellationToken);
}
