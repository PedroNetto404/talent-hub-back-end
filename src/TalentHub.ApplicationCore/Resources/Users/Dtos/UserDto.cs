namespace TalentHub.ApplicationCore.Users.Dtos;

public sealed record UserDto(
    Guid Id,
    string Email,
    string Username
)
{
    public static UserDto FromEntity(User user) =>
        new(
            user.Id,
            user.Email,
            user.Username
        );
}
