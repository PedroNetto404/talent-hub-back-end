using Microsoft.AspNetCore.Authorization;

namespace TalentHub.Infra.Security.Polices.CandidateOperations;

public sealed class CandidateOperationsRequirement : IAuthorizationRequirement;

public sealed class CandidateOperationsRequirementHandler : AuthorizationHandler<CandidateOperationsRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CandidateOperationsRequirement requirement
    )
    {
        if (context.User.HasClaim(c => c.Type == "CandidateOperations"))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
