using Ardalis.SmartEnum;

namespace TalentHub.ApplicationCore.Resources.Users.Enums;

public sealed class Role : SmartEnum<Role>
{
    private Role(string name, int value) : base(name, value)
    {
    }

    public static readonly Role Admin = new("admin", 1);
    public static readonly Role Company = new("company", 2);
    public static readonly Role Candidate = new("candidate", 3);

    public static implicit operator string(Role value) => value.Name;
}
