using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Candidates.Events;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.ApplicationCore.Candidates;

public sealed class Candidate : AggregateRoot
{
    public Candidate
    (
        string name,
        string email,
        string phone,
        DateOnly birthDate,
        Address address,
        string? instagramUrl,
        string? linkedinUrl,
        string? githubUrl,
        decimal? expectedRemuneration,
        string? summary
    )
    {
        Name = name;
        Email = email;
        Phone = phone;
        BirthDate = birthDate;
        Address = address;
        InstagramUrl = instagramUrl;
        LinkedinUrl = linkedinUrl;
        GithubUrl = githubUrl;
        ExpectedRemuneration = expectedRemuneration;
        Summary = summary;

        RaiseEvent(new CandidateCreatedEvent(Id));
    }

#pragma warning disable CS0628 // Novo membro protegido declarado no tipo selado
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
    protected Candidate()
    {
    }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
#pragma warning restore CS0628 // Novo membro protegido declarado no tipo selado

    private readonly List<CandidateSkill> _skills = [];
    private readonly List<LanguageProficiency> _languageProficiencies = [];
    private readonly List<string> _hobbies = [];
    private readonly List<Certificate> _certificates = [];
    private readonly List<Experience> _experiences = [];
    private readonly List<WorkplaceType> _desiredWorkplaceTypes = [];
    private readonly List<JobType> _desiredJobTypes = [];

    public string Name { get; private set; }
    public string? Summary { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string ProfilePictureFileName => $"candidate_profile_picture-{Id}";
    public string? ResumeUrl { get; private set; }
    public string ResumeFileName => $"candidate_resume-{Id}";
    public string? InstagramUrl { get; private set; }
    public string? LinkedinUrl { get; private set; }
    public string? GithubUrl { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public decimal? ExpectedRemuneration { get; private set; }
    public Address Address { get; private set; }
    public IEnumerable<CandidateSkill> Skills => _skills.AsReadOnly();
    public IEnumerable<LanguageProficiency> LanguageProficiencies => _languageProficiencies.AsReadOnly();
    public IEnumerable<string> Hobbies => _hobbies.AsReadOnly();
    public IEnumerable<Certificate> Certificates => _certificates.AsReadOnly();
    public IEnumerable<Experience> Experiences => _experiences.AsReadOnly();
    public IEnumerable<WorkplaceType> DesiredWorkplaceTypes => _desiredWorkplaceTypes.AsReadOnly();
    public IEnumerable<JobType> DesiredJobTypes => _desiredJobTypes.AsReadOnly();

    public int Age
    {
        get
        {
            var now = DateTime.Now;
            var diff = now - BirthDate.ToDateTime(TimeOnly.MinValue);

            return (int)Math.Floor(diff.TotalDays / 365);
        }
    }

    public Result SetResumeUrl(string resumeUrl)
    {
        if (string.IsNullOrWhiteSpace(resumeUrl))
            return new Error("candidate", "invalid resume url");

        ResumeUrl = resumeUrl;

        return Result.Ok();
    }

    public Result AddLanguage(LanguageProficiency languageProficiency)
    {
        var existing = _languageProficiencies.FirstOrDefault(p => p.Language == languageProficiency.Language);
        if (existing is not null) return new Error("candidate", "candidate language proficiency already exists");

        _languageProficiencies.Add(languageProficiency);
        return Result.Ok();
    }

    public Result UpdateLanguageProficiency(
        Language language,
        LanguageSkillType type,
        Proficiency proficiency)
    {
        var languageProficiency = _languageProficiencies.FirstOrDefault(p => p.Language == language);
        if (languageProficiency is null) return new Error("candidate", "language proficiency not added");

        languageProficiency.UpdateProficiency(type, proficiency);
        return Result.Ok();
    }

    public Result RemoveLanguage(Language language)
    {
        var languageProficiency = _languageProficiencies.FirstOrDefault(p => p.Language == language);
        if (languageProficiency is null) return new Error("candidate", "candidate language proficiency not exists");

        _languageProficiencies.Remove(languageProficiency);
        return Result.Ok();
    }

    public Result AddCertificate(Certificate certificate)
    {
        if (_certificates.Any(c => c.Name == certificate.Name))
        {
            return new Error("candidate_certificate", $"Certificate '{certificate.Name}' already exists.");
        }

        _certificates.Add(certificate);
        return Result.Ok();
    }

    public Result RemoveCertificate(string certificateName)
    {
        var existingCertificate = _certificates.FirstOrDefault(c => c.Name == certificateName);
        if (existingCertificate is null)
        {
            return new Error("candidate_certificate", $"Certificate '{certificateName}' not found.");
        }

        _certificates.Remove(existingCertificate);
        return Result.Ok();
    }

    public Result AddHobbie(string hobbie)
    {
        if (_hobbies.Contains(hobbie))
        {
            return new Error("candidate_hobbie", $"Hobbie '{hobbie}' already exists.");
        }

        _hobbies.Add(hobbie);
        return Result.Ok();
    }

    public void ClearHobbies() => _hobbies.Clear();

    public Result AddSkill(CandidateSkill skill)
    {
        if (_skills.Any(s => s.SkillId == skill.SkillId))
        {
            return new Error("candidate_skill", "skill already exists");
        }

        _skills.Add(skill);

        return Result.Ok();
    }

    public Result RemoveSkill(Guid candidateSkillId)
    {
        var existingSkill = _skills.FirstOrDefault(s => s.Id == candidateSkillId);
        if (existingSkill is null)
        {
            return new Error("candidate_skill", "candidate not have skill");
        }

        _skills.Remove(existingSkill);

        return Result.Ok();
    }

    public Result UpdateSkillProficiency(Guid skillId, Proficiency proficiency)
    {
        var existingSkill = _skills.FirstOrDefault(s => s.SkillId == skillId);
        if (existingSkill is null)
        {
            return new Error("candidate_skill", "Skill not added");
        }

        existingSkill.UpdateProficiency(proficiency);

        return Result.Ok();
    }

    public Result AddExperience(Experience experience)
    {
        if (_experiences.Any(e => e.Start == experience.Start && e.End == experience.End))
        {
            return new Error("candidate_experience", "An experience with the same period already exists.");
        }

        _experiences.Add(experience);
        return Result.Ok();
    }

    public Result RemoveExperience(Guid experienceId)
    {
        var experience = _experiences.FirstOrDefault(e => e.Id == experienceId);
        if (experience == null)
        {
            return new Error("candidate_experience", "Experience not found.");
        }

        _experiences.Remove(experience);
        return Result.Ok();
    }

    public Result UpdateAcademicExperienceProgressStatus(Guid academicExperienceId, ProgressStatus newStatus)
    {
        var academicExperience = _experiences.OfType<AcademicExperience>()
            .FirstOrDefault(ae => ae.Id == academicExperienceId);

        if (academicExperience == null)
        {
            return new Error("candidate_academic_experience", "Academic experience not found.");
        }

        academicExperience.UpdateStatus(newStatus);

        return Result.Ok();
    }

    public Result ChangeProfessionalExperienceDescription(Guid professionalExperienceId, string newDescription)
    {
        var professionalExperience = _experiences.OfType<ProfessionalExperience>()
            .FirstOrDefault(pe => pe.Id == professionalExperienceId);

        if (professionalExperience == null)
        {
            return new Error("candidate_professional_experience", "Professional experience not found.");
        }

        return professionalExperience.ChangeDescription(newDescription);
    }

    public Result AddDesiredJobType(JobType jobType)
    {
        if (_desiredJobTypes.Contains(jobType))
            return new Error("candidate_desired_job_type", "job type already added");

        _desiredJobTypes.Add(jobType);

        return Result.Ok();
    }

    public void ClearDesiredJobTypes() => _desiredJobTypes.Clear();

    public Result AddDesiredWorkplaceType(WorkplaceType workplaceType)
    {
        if (_desiredWorkplaceTypes.Contains(workplaceType))
            return new Error("candidate_desired_workplace_type", "workplace type already added");

        _desiredWorkplaceTypes.Add(workplaceType);
        return Result.Ok();
    }

    public void ClearDesiredWorkplaceTypes() => _desiredWorkplaceTypes.Clear();

    public Result UpdateProfilePicture(string profilePictureUrl)
    {
        if (!Uri.IsWellFormedUriString(profilePictureUrl, UriKind.Absolute))
            return new Error("candidate_profile_picture", "invalid profile picture url");

        ProfilePictureUrl = profilePictureUrl;
        return Result.Ok();
    }

    public Result UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new Error("candidate_name", "invalid name");

        Name = name;
        return Result.Ok();
    }

    public Result UpdatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 11)
            return new Error("candidate_phone", "invalid candidate phone");

        Phone = phone;
        RaiseEvent(new CandidatePhoneUpdatedEvent(Id));

        return Result.Ok();
    }

    public Result UpdateAddress(Address address)
    {
        if (
            typeof(Address)
            .GetProperties()
            .Any(p => string.IsNullOrWhiteSpace((string)p.GetValue(address)!))
        ) return new Error("candidate_address", "invalid address");

        Address = address;
        return Result.Ok();
    }

    public Result UpdateInstagramUrl(string instagramUrl)
    {
        if (!Uri.IsWellFormedUriString(instagramUrl, UriKind.Absolute))
            return new Error("candidate_instagram_url", "invalid url");

        InstagramUrl = instagramUrl;
        return Result.Ok();
    }

    public Result UpdateLinkedinUrl(string linkedinUrl)
    {
        if (!Uri.IsWellFormedUriString(linkedinUrl, UriKind.Absolute))
            return new Error("candidate_linkedin_url", "invalid url");

        InstagramUrl = linkedinUrl;
        return Result.Ok();
    }

    public Result UpdateGithubUrl(string githubUrl)
    {
        if (!Uri.IsWellFormedUriString(githubUrl, UriKind.Absolute))
            return new Error("candidate_Github_url", "invalid url");

        InstagramUrl = githubUrl;
        return Result.Ok();
    }

    public Result UpdateSummary(string summary)
    {
        if (string.IsNullOrWhiteSpace(summary))
            return new Error("candidate_summary", "invalid summary");

        if (summary.Length is < 10 or > 500)
            return new Error("candidate_summary", "candidate summary length must be between 10 and 500");

        Summary = summary;

        return Result.Ok();
    }

    public Result UpdateExperience(
        Guid experienceId,
        DatePeriod start,
        DatePeriod? end,
        int currentSemester,
        bool isCurrent,
        IEnumerable<string> activities,
        IEnumerable<string> academicEntities,
        ProgressStatus status
    ) => UpdateExperience<AcademicExperience>(
        experienceId,
        start,
        end,
        isCurrent,
        activities,
        experience =>
        {
            experience.ClearAcademicEntities();

            foreach (var academicEntity in academicEntities)
            {
                if (!Enum.TryParse<AcademicEntity>(academicEntity, true, out var entity))
                    return new Error("candidate_experience", "Invalid academic entity");

                if (experience.AddAcademicEntity(entity) is { IsFail: true, Error: var error })
                    return error;
            }

            experience.UpdateStatus(status);

            return experience.UpdateCurrentSemester(currentSemester) is { IsFail: true, Error: var err }
                ? err
                : Result.Ok();
        }
    );

    public Result UpdateExperience(
        Guid experienceId,
        DatePeriod start,
        DatePeriod? end,
        bool isCurrent,
        IEnumerable<string> activities,
        string description
    ) => UpdateExperience<ProfessionalExperience>(
        experienceId,
        start,
        end,
        isCurrent,
        activities,
        (experience) => experience.ChangeDescription(description)
    );

    private Result UpdateExperience<T>(
        Guid experienceId,
        DatePeriod start,
        DatePeriod? end,
        bool isCurrent,
        IEnumerable<string> activities,
        Func<T, Result> complement) where T : Experience
    {
        if (_experiences.FirstOrDefault(e => e.Id == experienceId) is not T experience)
            return new Error("candidate_experience", "Experience not found.");

        experience.ClearActivities();
        foreach (var activity in activities) experience.AddActivity(activity);

        var result = experience.UpdateDateRange(start, end);
        if (result.IsFail) return result.Error;

        experience.SetIsCurrent(isCurrent);

        if (isCurrent)
        {
            var professionalExperiences =
                _experiences.OfType<ProfessionalExperience>().Where(e => e.Id != experienceId);
            foreach (var professionalExperience in professionalExperiences)
            {
                professionalExperience.SetIsCurrent(false);
            }
        }

        if (complement(experience) is { IsFail: true, Error: var error }) return error;

        return Result.Ok();
    }
}