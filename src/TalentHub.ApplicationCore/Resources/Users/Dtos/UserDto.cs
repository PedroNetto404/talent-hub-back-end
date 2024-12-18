namespace TalentHub.ApplicationCore.Resources.Users.Dtos;

public sealed record UserDto(
    Guid Id,
    string Email,
    string Username,
    string? ProfilePictureUrl
)
{
    public static UserDto FromEntity(User user) =>
        new(
            user.Id,
            user.Email,
            user.Username,
            user.ProfilePictureUrl
        );
}
