using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users;
using TalentHub.ApplicationCore.Resources.Users.Enums;

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

    public bool IsCompany => httpContextAccessor.HttpContext?.User?.IsInRole(nameof(Role.Company)) ?? false;
    public bool IsCandidate => httpContextAccessor.HttpContext?.User?.IsInRole(nameof(Role.Candidate)) ?? false;
    public bool IsAdmin => httpContextAccessor.HttpContext?.User?.IsInRole(nameof(Role.Admin)) ?? false;

    public Task<User?> GetCurrentAsync(CancellationToken cancellationToken = default) =>
        UserId is null
        ? Task.FromResult<User?>(null)
        : userRepository.GetByIdAsync(UserId!.Value, cancellationToken);
}
