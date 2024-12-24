namespace TalentHub.ApplicationCore.Core.Abstractions;

public abstract class VersionedEntity : Entity
{
    public int? Version { get; private set; } = null;

    private void UpdateVersion()
    {
        if (Version is null)
        {
            Version = 0;
            return;
        }

        Version++;
    }
}
