using Humanizer;
using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.ApplicationCore.Skills;

namespace TalentHub.ApplicationCore.Candidates.Dtos;

public sealed record LanguageProficiencyDto(
    string Language,
    string WritingLevel,
    string SpeakingLevel,
    string ListeningLevel
)
{
    public static LanguageProficiencyDto FromEntity(LanguageProficiency languageProficiency) =>
        new(
            languageProficiency.Language.Name,
            languageProficiency.WritingLevel.ToString().Underscore(),
            languageProficiency.SpeakingLevel.ToString().Underscore(),
            languageProficiency.ListeningLevel.ToString().Underscore()
        );
}

public sealed record CandidateDto(
    Guid Id,
    string Name,
    int Age,
    DateOnly BirthDate,
    string Email,
    string Phone,
    Address Address,
    IEnumerable<string> Hobbies,
    IEnumerable<JobType> DesiredJobTypes,
    IEnumerable<WorkplaceType> DesiredWorkplaceTypes,
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
    public static CandidateDto FromEntity(Candidate candidate, IEnumerable<Skill> skills) =>
        new(
            candidate.Id,
            candidate.Name,
            candidate.Age,
            candidate.BirthDate,
            candidate.Email,
            candidate.Phone,
            candidate.Address,
            candidate.Hobbies,
            candidate.DesiredJobTypes,
            candidate.DesiredWorkplaceTypes,
            candidate.Experiences.Select(ExperienceDto.FromEntity),
            candidate.Skills.Select((candidateSkill) =>
            {
                var skill = skills.First(s => s.Id == candidateSkill.SkillId);
                return CandidateSkillDto.FromEntity(candidateSkill, skill);
            }),
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
