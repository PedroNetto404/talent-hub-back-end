using Humanizer;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

public sealed record CandidateDto(
    Guid Id,
    string Name,
    Guid UserId,
    int Age,
    DateOnly BirthDate,
    string Phone,
    Address Address,
    IEnumerable<string> Hobbies,
    IEnumerable<string> DesiredJobTypes,
    IEnumerable<string> DesiredWorkplaceTypes,
    IEnumerable<ExperienceDto> Experiences,
    IEnumerable<CandidateSkillDto> Skills,
    IEnumerable<CertificateDto> Certificates,
    IEnumerable<LanguageProficiencyDto> LanguageProficiencies,
    string? Summary,
    string? ResumeUrl,
    string? ProfilePictureUrl,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? GithubUrl,
    decimal? ExpectedRemuneration
)
{
    public static CandidateDto FromEntity(Candidate candidate) =>
        new(
            candidate.Id,
            candidate.Name,
            candidate.UserId,
            candidate.Age,
            candidate.BirthDate,
            candidate.Phone,
            candidate.Address,
            candidate.Hobbies,
            candidate.DesiredJobTypes.Select(p => p.ToString().Underscore()),
            candidate.DesiredWorkplaceTypes.Select(p => p.ToString().Underscore()),
            candidate.Experiences.Select(ExperienceDto.FromEntity),
            candidate.Skills.Select(CandidateSkillDto.FromEntity),
            candidate.Certificates.Select(CertificateDto.FromEntity),
            candidate.LanguageProficiencies.Select(LanguageProficiencyDto.FromEntity),
            candidate.Summary,
            candidate.ResumeUrl,
            candidate.ProfilePictureUrl,
            candidate.InstagramUrl,
            candidate.LinkedinUrl,
            candidate.GithubUrl,
            candidate.ExpectedRemuneration
        );
}
