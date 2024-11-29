using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Users.Specs;

public sealed class GetUserByEmailOrUsernameSpec : Specification<User>
{
    public GetUserByEmailOrUsernameSpec(
        string email,
        string username
    )
    {
        Query
            .Where(user => user.Email == email || user.Username == username)
            .AsNoTracking();
    }
}
