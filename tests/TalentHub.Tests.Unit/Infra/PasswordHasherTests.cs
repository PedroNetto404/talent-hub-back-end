
using TalentHub.Infra.Security.Services;

namespace TalentHub.Tests.Unit.Infra;
public class PasswordHasherTests
{
    private readonly Sha256Hasher _passwordHasher;

    public PasswordHasherTests()
    {
        _passwordHasher = new Sha256Hasher();
    }

    [Fact]
    public void Hash_ShouldReturnHashedString_WhenPasswordIsValid()
    {
        // Arrange
        string password = "TestPassword123";

        // Act
        string result = _passwordHasher.Hash(password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(64, result.Length); // SHA256 hash length is 64 characters in hex.
    }

    [Fact]
    public void Hash_ShouldThrowArgumentException_WhenPasswordIsNull()
    {
        // Arrange
        string password = null;

        // Act & Assert
        Exception exception = Assert.Throws<ArgumentException>(() => _passwordHasher.Hash(password!));
        Assert.Equal("Password cannot be null or empty. (Parameter 'password')", exception.Message);
    }

    [Fact]
    public void Hash_ShouldThrowArgumentException_WhenPasswordIsEmpty()
    {
        // Arrange
        string password = "";

        // Act & Assert
        Exception exception = Assert.Throws<ArgumentException>(() => _passwordHasher.Hash(password));
        Assert.Equal("Password cannot be null or empty. (Parameter 'password')", exception.Message);
    }

    [Fact]
    public void Match_ShouldReturnTrue_WhenPasswordsMatch()
    {
        // Arrange
        string password = "ValidPassword123";
        string hashedPassword = _passwordHasher.Hash(password);

        // Act
        bool result = _passwordHasher.Match(password, hashedPassword);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Match_ShouldReturnFalse_WhenPasswordsDoNotMatch()
    {
        // Arrange
        string password = "ValidPassword123";
        string wrongPassword = "WrongPassword456";
        string hashedPassword = _passwordHasher.Hash(password);

        // Act
        bool result = _passwordHasher.Match(wrongPassword, hashedPassword);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Match_ShouldReturnFalse_WhenPasswordOrStoredHashIsNull()
    {
        // Arrange
        string password = "SomePassword";
        string hashedPassword = null!;

        // Act & Assert
        Assert.False(_passwordHasher.Match(password, hashedPassword));
        Assert.False(_passwordHasher.Match(null!, hashedPassword));
    }

    [Fact]
    public void Match_ShouldBeCaseInsensitive_ForHexHashComparison()
    {
        // Arrange
        string password = "ValidPassword123";
        string hashedPassword = _passwordHasher.Hash(password).ToUpper();

        // Act
        bool result = _passwordHasher.Match(password, hashedPassword);

        // Assert
        Assert.True(result);
    }
}
