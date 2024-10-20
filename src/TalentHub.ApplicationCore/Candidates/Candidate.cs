using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared;

namespace TalentHub.ApplicationCore.Candidates;

public sealed class Candidate : AggregateRoot
{
    private readonly List<CandidateSkill> _skills = [];

    public string Name { get; private set; }
    public string Summary { get; private set; }
    public string ResumeUrl { get; private set; }
    public string? InstagramUrl { get; private set; }
    public string? LinkedinUrl { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public decimal ExpectedRemuneration { get; private set; }
    public Address Address { get; private set; }
    public JobType DesiredJobType { get; private set; }
    public WorkplaceType DesiredWorkPlaceType { get; private set; }
    public IReadOnlyCollection<CandidateSkill> Skills => _skills.AsReadOnly();

    public int Age
    {
        get
        {
            var now = DateTime.Now;
            var diff = now - BirthDate.ToDateTime(TimeOnly.MinValue);

            return (int)Math.Floor(diff.TotalDays / 365);
        }
    }

    public Result AddSkill(CandidateSkill skill)
    {
        if (_skills.Any(s => s.SkillId == skill.SkillId))
        {
            return Error.Displayable("candidate_skill", $"skill {skill.SkillName} already exists");
        }

        _skills.Add(skill);

        return Result.Ok();
    }

    public Result RemoveSkill(Guid candidateSkillId)
    {
        var existingSkill = _skills.FirstOrDefault(s => s.Id == candidateSkillId);
        if (existingSkill is null)
        {
            return Error.Displayable("candidate_skill", "candidate not have skill");
        }

        _skills.Remove(existingSkill);

        return Result.Ok();
    }

    public Result UpdateLanguageSkillSpecialProficiency(
        Guid candidateLanguageSkillId,
        LanguageSkillType languageSkillType,
        Proficiency proficiency)
    {
        if (_skills.FirstOrDefault(s => s.Id == candidateLanguageSkillId) is not CandidateLanguagueSkill skill)
        {
            return Error.Displayable("candidate_skill", "Language not added");
        }

        skill.UpdateSpecialProficiency(languageSkillType, proficiency);

        return Result.Ok();
    }

    public Result UpdateSkillProficiency(Guid candidateSkillId, Proficiency proficiency)
    {
        var existingSkill = _skills.FirstOrDefault(s => s.Id == candidateSkillId);
        if (existingSkill is null)
        {
            return Error.Displayable("candidate_skill", "Skill not added");
        }

        existingSkill.UpdateProficiency(proficiency);

        return Result.Ok();
    }
}
