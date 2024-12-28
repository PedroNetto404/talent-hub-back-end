using Ardalis.Specification;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.Users.Specs;

public sealed class GetUsersSpec : GetPageSpec<User>
{
    public GetUsersSpec(
        string? usernameLike,
        string? emailLike,
        Role? role,
        int limit,
        int offset,
        string? sortBy,
        SortOrder sortOrder
    ) : base(limit, offset, sortBy, sortOrder)
    {
        if (!string.IsNullOrWhiteSpace(usernameLike))
        {
            Query.Where(u => u.Username.Contains(usernameLike));
        }

        if (!string.IsNullOrWhiteSpace(emailLike))
        {
            Query.Where(u => u.Email.Contains(emailLike));
        }

        if (role is not null)
        {
            Query.Where(p => p.Role == role);
        }
    }
}
