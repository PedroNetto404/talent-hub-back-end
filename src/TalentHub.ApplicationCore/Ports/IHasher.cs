namespace TalentHub.ApplicationCore.Ports;

public interface IHasher
{
    string Hash(string plainText);

    bool Match(string plainText, string hash);
}
