using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates;

namespace TalentHub.ApplicationCore.Resources.Candidates.Dtos;

public sealed record CertificateDto(
    Guid Id,
    string Name,
    string Institution,
    double Workload,
    string? Url,
    IEnumerable<Guid> RelatedSkills
)
{
    public static CertificateDto FromEntity(Certificate certificate) =>
        new(
            certificate.Id,
            certificate.Name,
            certificate.Issuer,
            certificate.Workload,
            certificate.AttachmentUrl, 
            certificate.RelatedSkills
        );
}
