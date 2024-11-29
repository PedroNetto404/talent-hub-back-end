namespace TalentHub.ApplicationCore.Ports;

public interface IPasswordHasher
{
    string Hash(string password);

    bool Match(string password, string hash);
}
