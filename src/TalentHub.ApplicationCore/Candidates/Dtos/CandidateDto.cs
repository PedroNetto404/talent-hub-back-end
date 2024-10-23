using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

public sealed record CandidateDto(
    Guid Id,
    string Name,
    int Age,
    DateOnly BirthDate,
    string Email,
    string Phone,
    Address Address,
    JobType DesiredJobType,
    WorkplaceType DesiredWorkplaceType,
    string? Summary,
    string? ResumeUrl,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? GithubUrl,
    decimal? ExpectedRemuneration,
    IEnumerable<CandidateSkillDto> Skills,
    IEnumerable<ExperienceDto> Experiences,
    IEnumerable<CertificateDto> Certificates
)
{
    public static CandidateDto FromEntity(Candidate candidate) =>
        new(
            candidate.Id,
            candidate.Name,
            candidate.Age,
            candidate.BirthDate,
            candidate.Email,
            candidate.Phone,
            candidate.Address,
            candidate.DesiredJobType,
            candidate.DesiredWorkPlaceType,
            candidate.Summary,
            candidate.ResumeUrl,
            candidate.InstagramUrl,
            candidate.LinkedinUrl,
            candidate.GithubUrl,
            candidate.ExpectedRemuneration,
            candidate.Skills.Select(CandidateSkillDto.FromEntity),
            candidate.Experiences.Select(ExperienceDto.FromEntity),
            candidate.Certificates.Select(CertificateDto.FromEntity)
        );
}
