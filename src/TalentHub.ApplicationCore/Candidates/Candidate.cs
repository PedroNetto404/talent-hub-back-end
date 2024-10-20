using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Candidates.ValueObjects;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Candidates;

public sealed class Candidate : AggregateRoot
{
    private readonly List<CandidateSkill> _skills = [];
    private readonly List<string> _hobbies = [];
    private readonly List<Certificate> _certificates = [];
    private readonly List<Experience> _experiences = [];

    public string Name { get; private set; }
    public string ProfilePictureUrl { get; private set; }
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
    public IReadOnlyCollection<string> Hobbies => _hobbies.AsReadOnly();
    public IReadOnlyCollection<Certificate> Certificates => _certificates.AsReadOnly();
    public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();

    public int Age
    {
        get
        {
            var now = DateTime.Now;
            var diff = now - BirthDate.ToDateTime(TimeOnly.MinValue);

            return (int)Math.Floor(diff.TotalDays / 365);
        }
    }

    public Result AddCertificate(Certificate certificate)
    {
        if (_certificates.Any(c => c.Name == certificate.Name))
        {
            return Error.Displayable("candidate_certificate", $"Certificate '{certificate.Name}' already exists.");
        }

        _certificates.Add(certificate);
        return Result.Ok();
    }

    public Result RemoveCertificate(string certificateName)
    {
        var existingCertificate = _certificates.FirstOrDefault(c => c.Name == certificateName);
        if (existingCertificate is null)
        {
            return Error.Displayable("candidate_certificate", $"Certificate '{certificateName}' not found.");
        }

        _certificates.Remove(existingCertificate);
        return Result.Ok();
    }


    public Result AddHobbie(string hobbie)
    {
        if (_hobbies.Contains(hobbie))
        {
            return Error.Displayable("candidate_hobbie", $"Hobbie '{hobbie}' already exists.");
        }

        _hobbies.Add(hobbie);
        return Result.Ok();
    }

    public Result RemoveHobbie(string hobbie)
    {
        if (!_hobbies.Contains(hobbie))
        {
            return Error.Displayable("candidate_hobbie", $"Hobbie '{hobbie}' not found.");
        }

        _hobbies.Remove(hobbie);
        return Result.Ok();
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

    public Result AddExperience(Experience experience)
    {
        if (_experiences.Any(e => e.Start == experience.Start && e.End == experience.End))
        {
            return Error.Displayable("candidate_experience", "An experience with the same period already exists.");
        }

        _experiences.Add(experience);
        return Result.Ok();
    }

    public Result RemoveExperience(Guid experienceId)
    {
        var experience = _experiences.FirstOrDefault(e => e.Id == experienceId);
        if (experience == null)
        {
            return Error.Displayable("candidate_experience", "Experience not found.");
        }

        _experiences.Remove(experience);
        return Result.Ok();
    }

    public Result ToggleCurrentExperience(Guid experienceId)
    {
        var experience = _experiences.FirstOrDefault(e => e.Id == experienceId);
        if (experience == null)
        {
            return Error.Displayable("candidate_experience", "Experience not found.");
        }

        experience.ToggleCurrent();

        if (experience.IsCurrent)
        {
            foreach (var otherExperience in _experiences.Where(e => e.Id != experienceId && e.IsCurrent))
            {
                otherExperience.ToggleCurrent();
            }
        }

        return Result.Ok();
    }

    public Result UpdateAcademicExperienceProgressStatus(Guid academicExperienceId, ProgressStatus newStatus)
    {
        var academicExperience = _experiences.OfType<AcademicExperience>().FirstOrDefault(ae => ae.Id == academicExperienceId);

        if (academicExperience == null)
        {
            return Error.Displayable("candidate_academic_experience", "Academic experience not found.");
        }

        academicExperience.UpdateStatus(newStatus);

        return Result.Ok();
    }

     public Result ChangeProfessionalExperienceDescription(Guid professionalExperienceId, string newDescription)
    {
        var professionalExperience = _experiences.OfType<ProfessionalExperience>().FirstOrDefault(pe => pe.Id == professionalExperienceId);

        if (professionalExperience == null)
        {
            return Error.Displayable("candidate_professional_experience", "Professional experience not found.");
        }

        return professionalExperience.ChangeDescription(newDescription);
    }
}
