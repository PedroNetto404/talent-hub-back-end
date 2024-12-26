using System;
using System.Security.Cryptography;
using System.Text;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.Infra.Security.Services;

public sealed class Sha256Hasher : IHasher
{
    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));
        }

        byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    public bool Match(string password, string storedHash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
        {
            return false;
        }

        string hashedPassword = Hash(password);

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(hashedPassword),
            Encoding.UTF8.GetBytes(storedHash)
        );
    }
}
