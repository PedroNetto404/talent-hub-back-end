using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.ApplicationCore.Resources.Users.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Users;

public sealed class User : AuditableAggregateRoot
{
    public const int PasswordMinLength = 8;
    public const int PasswordMaxLength = 20;

    private User(
        string email,
        string username,
        string hashedPassword,
        Role role
    )
    {
        Email = email;
        Username = username;
        HashedPassword = hashedPassword;
        Role = role;
    }

    public static Result<User> Create(
        string email,
        string username,
        Role role,
        string password,
        IPasswordHasher passwordHasher
    )
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return new Error("user", "invalid user email");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            return new Error("user", "invalid user name");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return new Error("user", "invalid user password");
        }

        string passwordHash = passwordHasher.Hash(password);

        return new User(
            email,
            username,
            passwordHash,
            role
        );
    }

    public string Email { get; private set; }
    public string Username { get; set; }
    public Role Role { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string HashedPassword { get; private set; }
    public Token? RefreshToken { get; private set; }

    public string ProfilePictureFileName => $"user-profile-picture-{Id}";

    public Result SetRefreshToken(
        Token refreshToken, 
        IDateTimeProvider dateTimeProvider
    )
    {
        if (refreshToken.IsExpired(dateTimeProvider))
        {
            return new Error("user", "expired refresh token");
        }

        RefreshToken = refreshToken;

        return Result.Ok();
    }

    public Result ValidateRefreshToken(
        string refreshToken,
        IDateTimeProvider dateTimeProvider
    )
    {
        if (RefreshToken is null)
        {
            return Result.Fail(new Error("user", "No refresh token available."));
        }

        if (RefreshToken.Value != refreshToken)
        {
            return Result.Fail(new Error("user", "Invalid refresh token."));
        }

        if (RefreshToken.IsExpired(dateTimeProvider))
        {
            return Result.Fail(new Error("user", "Refresh token has expired."));
        }

        return Result.Ok();
    }

    public bool CanRefreshToken(IDateTimeProvider dateTimeProvider) =>
        RefreshToken is not null &&
        !RefreshToken.IsExpired(dateTimeProvider);

    public Result ChangeProfilePicture(string? profilePictureUrl)
    {
        if (profilePictureUrl is null)
        {
            ProfilePictureUrl = null;
            return Result.Ok();
        }

        if (string.IsNullOrWhiteSpace(profilePictureUrl))
        {
            return Error.BadRequest("profile picture url cannot be empty");
        }

        if (!profilePictureUrl.IsValidUrl())
        {
            return Error.BadRequest("profile picture url is not valid url");
        }

        ProfilePictureUrl = profilePictureUrl;
        return Result.Ok();
    }

#pragma warning disable CS0628 // New protected member declared in sealed type
    protected User() { }
#pragma warning restore CS0628 // New protected member declared in sealed type
}
