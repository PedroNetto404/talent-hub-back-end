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
    public Guid UserId => httpContextAccessor.HttpContext!.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value is string userId
        ? Guid.Parse(userId)
        : throw new InvalidOperationException("user not authenticated");
        
    public async Task<Result<User>> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        User? user = await userRepository.GetByIdAsync(UserId, cancellationToken);
        if (user is null)
        {
            return Error.NotFound("user");
        }

        return user;
    }
}
