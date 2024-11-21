using TalentHub.ApplicationCore.Candidates.Entities;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

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
            certificate.Institution,
            certificate.Workload,
            certificate.Url
        );
}