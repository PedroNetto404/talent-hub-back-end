using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Users.Specs;

public sealed class GetUserByEmailSpec : Specification<User>
{
    public GetUserByEmailSpec(string email)
    {
        Query.Where(p => p.Email == email).AsNoTracking();
    }
}
