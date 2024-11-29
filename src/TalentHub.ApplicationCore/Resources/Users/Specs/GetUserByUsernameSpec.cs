using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Users.Specs;

public sealed class GetUserByUsernameSpec : Specification<User>
{
    public GetUserByUsernameSpec(string username)
    {
        Query.Where(p => p.Username == username).AsNoTracking();
    }
}
