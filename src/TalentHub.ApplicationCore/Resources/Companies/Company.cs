using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Companies;

public sealed class Company : AuditableAggregateRoot
{
    private readonly List<string> _galery = [];

    public string LegalName { get; private set; }
    public string TradeName { get; private set; }
    public string Cnpj { get; private set; }
    public Guid SectorId { get; private set; }
    public string RecruitmentEmail { get; private set; }
    public string? Phone { get; private set; }   
    public bool AutoMatchEnabled { get; private set; } = true;
    public int EmployeeCount { get; set; }
    public string? LogoUrl { get; private set; }
    public string? SiteUrl { get; private set; }
    public Address Address { get; private set; }
    public string? About { get; private set; }
    public string? InstagramUrl { get; private set; }
    public string? FacebookUrl { get; private set; }
    public string? LinkedinUrl { get; private set; }
    public string? CareerPageUrl { get; private set; }
    public string? PresentationVideoUrl { get; private set; }
    public string? Mission { get; private set; }
    public string? Vision { get; private set; }
    public string? Values { get; private set; }
    public int FoundantionYear { get; private set; }
    public IReadOnlyList<string> Gelery => _galery.AsReadOnly();
}
