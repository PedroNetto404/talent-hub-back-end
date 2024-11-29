using System;
using System.Security.Cryptography;
using System.Text;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.Security.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        { throw new ArgumentException("Password cannot be null or empty.", nameof(password)); }

        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        var stringBuilder = new StringBuilder();
        foreach (byte b in bytes)
        {
            stringBuilder.Append(b.ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    public bool Match(string password, string storedHash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
        { return false; }

        string hashedPassword = Hash(password);
        return hashedPassword.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
    }
}
