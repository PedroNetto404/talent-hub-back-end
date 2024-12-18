using System;
using Ardalis.Specification;

namespace TalentHub.ApplicationCore.Resources.Users.Specs;

public sealed class GetUserByEmailOrUsernameSpec : Specification<User>
{
    public GetUserByEmailOrUsernameSpec(
        string? email,
        string? username
    )
    {
        Query.Where(u =>
            u.Email == email ||
            u.Username == username
        );
    }
}
