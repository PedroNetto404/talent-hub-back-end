using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Companies;

public sealed class Company : AggregateRoot
{
    private readonly List<string> _galery = [];

    public string Name { get; private set; }
    public string SiteUrl { get; private set; }
    public string? About { get; private set; }
    public string? LinkedinUrl { get; private set; }
    public IReadOnlyList<string> Gelery => _galery.AsReadOnly();
    public int FoundantionYear { get; private set; }
}
