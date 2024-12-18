using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates;

namespace TalentHub.ApplicationCore.Resources.Candidates.Dtos;

public sealed record CertificateDto(
    string Name,
    string Institution,
    double Workload,
    string? Url
)
{
    public static CertificateDto FromEntity(Certificate certificate) =>
        new(
            certificate.Name,
            certificate.Issuer,
            certificate.Workload,
            certificate.AttachmentUrl
        );
}
