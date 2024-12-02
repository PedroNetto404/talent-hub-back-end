using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.Candidates.Entities;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.Events;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates;

public sealed class Candidate : AuditableAggregateRoot
{
    public Candidate
    (
        string name,
        Guid userId,
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
        UserId = userId;
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
    protected Candidate()
    {
    }
#pragma warning restore CS0628 // Novo membro protegido declarado no tipo selado

    private readonly List<CandidateSkill> _skills = [];
    private readonly List<LanguageProficiency> _languageProficiencies = [];
    private readonly List<string> _hobbies = [];
    private readonly List<Certificate> _certificates = [];
    private readonly List<Experience> _experiences = [];
    private readonly List<WorkplaceType> _desiredWorkplaceTypes = [];
    private readonly List<JobType> _desiredJobTypes = [];

    public string Name { get; private set; }
    public Guid UserId { get; private set; }
    public string? Summary { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public string ProfilePictureFileName => $"candidate_profile_picture-{Id}";
    public string? ResumeUrl { get; private set; }
    public string ResumeFileName => $"candidate_resume-{Id}";
    public string? InstagramUrl { get; private set; }
    public string? LinkedinUrl { get; private set; }
    public string? GithubUrl { get; private set; }
    public DateOnly BirthDate { get; private set; }
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
            DateTime now = DateTime.Now;
            TimeSpan diff = now - BirthDate.ToDateTime(TimeOnly.MinValue);

            return (int)Math.Floor(diff.TotalDays / 365);
        }
    }

    public Result SetResumeUrl(string resumeUrl)
    {
        if (string.IsNullOrWhiteSpace(resumeUrl))
        {
            return new Error("candidate", "invalid resume url");
        }

        ResumeUrl = resumeUrl;

        return Result.Ok();
    }

    public Result AddLanguage(LanguageProficiency languageProficiency)
    {
        LanguageProficiency? existing =
            _languageProficiencies.FirstOrDefault(p => p.Language == languageProficiency.Language);
        if (existing is not null)
        {
            return new Error("candidate", "candidate language proficiency already exists");
        }

        _languageProficiencies.Add(languageProficiency);
        return Result.Ok();
    }

    public Result UpdateLanguageProficiency(
        Language language,
        LanguageSkillType type,
        Proficiency proficiency)
    {
        LanguageProficiency? languageProficiency = _languageProficiencies.FirstOrDefault(p => p.Language == language);
        if (languageProficiency is null)
        {
            return new Error("candidate", "language proficiency not added");
        }

        languageProficiency.UpdateProficiency(type, proficiency);
        return Result.Ok();
    }

    public Result RemoveLanguage(Language language)
    {
        LanguageProficiency? languageProficiency = _languageProficiencies.FirstOrDefault(p => p.Language == language);
        if (languageProficiency is null)
        {
            return new Error("candidate", "candidate language proficiency not exists");
        }

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

    public Result RemoveCertificate(Guid certificateId)
    {
        Certificate? existingCertificate = _certificates.FirstOrDefault(c => c.Id == certificateId);
        if(existingCertificate is null)
        {
            return Error.BadRequest("invalid certificate id");
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
        CandidateSkill? existingSkill = _skills.FirstOrDefault(s => s.Id == candidateSkillId);
        if (existingSkill is null)
        {
            return new Error("candidate_skill", "candidate not have skill");
        }

        _skills.Remove(existingSkill);

        return Result.Ok();
    }

    public Result UpdateSkillProficiency(Guid skillId, Proficiency proficiency)
    {
        CandidateSkill? existingSkill = _skills.FirstOrDefault(s => s.SkillId == skillId);
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
        Experience? experience = _experiences.FirstOrDefault(e => e.Id == experienceId);
        if (experience == null)
        {
            return new Error("candidate_experience", "Experience not found.");
        }

        _experiences.Remove(experience);
        return Result.Ok();
    }

    public Result UpdateAcademicExperienceProgressStatus(Guid academicExperienceId, ProgressStatus newStatus)
    {
        AcademicExperience? academicExperience =
            _experiences
                .OfType<AcademicExperience>()
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
        ProfessionalExperience? professionalExperience =
            _experiences
                .OfType<ProfessionalExperience>()
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
        {
            return new Error("candidate_desired_job_type", "job type already added");
        }

        _desiredJobTypes.Add(jobType);

        return Result.Ok();
    }

    public void ClearDesiredJobTypes() => _desiredJobTypes.Clear();

    public Result AddDesiredWorkplaceType(WorkplaceType workplaceType)
    {
        if (_desiredWorkplaceTypes.Contains(workplaceType))
        {
            return new Error("candidate_desired_workplace_type", "workplace type already added");
        }

        _desiredWorkplaceTypes.Add(workplaceType);
        return Result.Ok();
    }

    public void ClearDesiredWorkplaceTypes() => _desiredWorkplaceTypes.Clear();

    public Result UpdateProfilePicture(string profilePictureUrl)
    {
        if (!Uri.IsWellFormedUriString(profilePictureUrl, UriKind.Absolute))
        {
            return new Error("candidate_profile_picture", "invalid profile picture url");
        }

        ProfilePictureUrl = profilePictureUrl;
        return Result.Ok();
    }

    public Result ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new Error("candidate_name", "invalid name");
        }

        Name = name;
        return Result.Ok();
    }

    public Result ChangePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 11)
        {
            return new Error("candidate_phone", "invalid candidate phone");
        }

        Phone = phone;
        RaiseEvent(new CandidatePhoneUpdatedEvent(Id));

        return Result.Ok();
    }

    public Result ChangeAddress(Address address)
    {
        if (
            typeof(Address)
            .GetProperties()
            .Any(p => string.IsNullOrWhiteSpace((string)p.GetValue(address)!))
        )
        {
            return new Error("candidate_address", "invalid address");
        }


        Address = address;
        return Result.Ok();
    }

    public Result UpdateInstagramUrl(string instagramUrl)
    {
        if (!Uri.IsWellFormedUriString(instagramUrl, UriKind.Absolute))
        {
            return new Error("candidate_instagram_url", "invalid url");
        }

        InstagramUrl = instagramUrl;
        return Result.Ok();
    }

    public Result UpdateLinkedinUrl(string linkedinUrl)
    {
        if (!Uri.IsWellFormedUriString(linkedinUrl, UriKind.Absolute))
        {
            return new Error("candidate_linkedin_url", "invalid url");
        }

        InstagramUrl = linkedinUrl;
        return Result.Ok();
    }

    public Result UpdateGithubUrl(string githubUrl)
    {
        if (!Uri.IsWellFormedUriString(githubUrl, UriKind.Absolute))
        {
            return new Error("candidate_Github_url", "invalid url");
        }

        InstagramUrl = githubUrl;
        return Result.Ok();
    }

    public Result UpdateSummary(string summary)
    {
        if (string.IsNullOrWhiteSpace(summary))
        {
            return new Error("candidate_summary", "invalid summary");
        }

        if (summary.Length is < 10 or > 500)
        {
            return new Error("candidate_summary", "candidate summary length must be between 10 and 500");
        }

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

            foreach (string academicEntity in academicEntities)
            {
                if (!Enum.TryParse(academicEntity, true, out AcademicEntity entity))
                {
                    return new Error("candidate_experience", "Invalid academic entity");
                }

                if (experience.AddAcademicEntity(entity) is { IsFail: true, Error: var error })
                {
                    return error;
                }
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
        {
            return Error.NotFound("candidate_experience");
        }

        experience.ClearActivities();
        foreach (string activity in activities)
        {
            experience.AddActivity(activity);
        }

        Result result = experience.UpdateDateRange(start, end);
        if (result.IsFail)
        {
            return result.Error;
        }

        experience.SetIsCurrent(isCurrent);

        if (isCurrent)
        {
            IEnumerable<ProfessionalExperience> professionalExperiences =
                _experiences
                    .OfType<ProfessionalExperience>()
                    .Where(e => e.Id != experienceId);

            foreach (ProfessionalExperience professionalExperience in professionalExperiences)
            {
                professionalExperience.SetIsCurrent(false);
            }
        }

        return complement(experience) is not { IsFail: true, Error: var error }
            ? Result.Ok()
            : error;
    }

    public Result ChangeExpectedRemuneration(decimal? expectedRemuniration)
    {
        if (expectedRemuniration is null)
        {
            ExpectedRemuneration = null;
            return Result.Ok();
        }

        if (expectedRemuniration <= 0)
        {
            return Error.BadRequest("expected remuneration must greater than 0");
        }

        ExpectedRemuneration = expectedRemuniration;

        return Result.Ok();
    }

    public Result ChangeInstagramUrl(string? instagramUrl) =>
        ChangeUrl(instagramUrl, (url) => InstagramUrl = url);

    public Result ChangeLinkedinUrl(string? linkedinUrl) =>
        ChangeUrl(linkedinUrl, (url) => LinkedinUrl = url);

    public Result ChangeGithubUrl(string? githubUrl) =>
        ChangeUrl(githubUrl, (url) => GithubUrl = url);

    private Result ChangeUrl(string? url, Action<string?> setter)
    {
        if (url is null)
        {
            setter(null);
            return Result.Ok();
        }

        if (url == string.Empty)
        {
            return Error.BadRequest("url cannot be blank");
        }

        if (!url.IsValidUrl())
        {
            return Error.BadRequest($"{url} is not valid url");
        }

        setter(url);

        return Result.Ok();
    }

    public Result ChangeSummary(string? summary)
    {
        if (summary is null)
        {
            Summary = null;
            return Result.Ok();
        }

        if (summary == string.Empty)
        {
            return Error.BadRequest("invalid summary");
        }

        Summary = summary;

        return Result.Ok();
    }

    public Result UpdateCertificate(
        Guid certificateId,
        string name,
        string issuer,
        double workload,
        string? url,
        IEnumerable<Guid> relatedSkills
    )
    {
        Certificate? certificate = _certificates.FirstOrDefault(
            p => p.Id == certificateId
        );
        if (certificate is null)
        {
            return Error.NotFound("certificate");
        }

        return Result.FailEarly(
            () => certificate.ChangeName(name),
            () => certificate.ChangeIssuer(issuer),
            () => certificate.ChangeWorkload(workload),
            () => certificate.ChangeUrl(url),
            () =>
            {
                certificate.ClearRelatedSkills();
                foreach (Guid skillId in relatedSkills)
                {
                    if (certificate.AddRelatedSkill(skillId) is
                        {
                            IsFail: true,
                            Error: var err
                        })
                    {
                        return err;
                    }
                }

                return Result.Ok();
            }
        );
    }
}
