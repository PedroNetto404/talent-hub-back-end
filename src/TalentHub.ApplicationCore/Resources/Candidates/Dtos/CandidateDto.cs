using Humanizer;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.Dtos;

public sealed record CandidateDto(
    Guid Id,
    Guid UserId,
    string Name,
    int Age,
    string? Summary,
    bool AutoMatchEnabled,
    string Phone,
    string? ResumeUrl,
    DateOnly BirthDate,
    decimal? ExpectedRemuneration,
    string? InstagramUrl,
    string? LinkedInUrl,
    string? GitHubUrl,
    Address Address,
    IEnumerable<string> Hobbies,
    IEnumerable<string> DesiredJobTypes,
    IEnumerable<string> DesiredWorkplaceTypes,
    IEnumerable<ExperienceDto> Experiences,
    IEnumerable<CandidateSkillDto> Skills,
    IEnumerable<CertificateDto> Certificates,
    IEnumerable<LanguageProficiencyDto> LanguageProficiencies
)
{
    public static CandidateDto FromEntity(Candidate candidate) =>
        new(
            candidate.Id,
            candidate.UserId,
            candidate.Name,
            candidate.Age,
            candidate.Summary,
            candidate.AutoMatchEnabled,
            candidate.Phone,
            candidate.ResumeUrl,
            candidate.BirthDate,
            candidate.ExpectedRemuneration,
            candidate.InstagramUrl,
            candidate.LinkedInUrl,
            candidate.GithubUrl,
            candidate.Address,
            candidate.Hobbies,
            candidate.DesiredJobTypes.Select(p => p.ToString().Underscore()),
            candidate.DesiredWorkplaceTypes.Select(p => p.ToString().Underscore()),
            candidate.Experiences.Select(ExperienceDto.FromEntity),
            candidate.Skills.Select(CandidateSkillDto.FromEntity),
            candidate.Certificates.Select(CertificateDto.FromEntity),
            candidate.LanguageProficiencies.Select(LanguageProficiencyDto.FromEntity)
        );
}
