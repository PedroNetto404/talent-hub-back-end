using TalentHub.ApplicationCore.Candidates.Entities;
using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Candidates.Events;
using TalentHub.ApplicationCore.Candidates.ValueObjects;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates;

public sealed class Candidate : AggregateRoot
{
    public Candidate(
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
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        BirthDate = birthDate;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        InstagramUrl = instagramUrl;
        LinkedinUrl = linkedinUrl;
        GithubUrl = githubUrl;
        ExpectedRemuneration = expectedRemuneration;
        Summary = summary;

        RaiseEvent(new CandidateCreatedEvent(Id));
    }

#pragma warning disable CS0628 // Novo membro protegido declarado no tipo selado
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
    protected Candidate() { }
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere adicionar o modificador "obrigatório" ou declarar como anulável.
#pragma warning restore CS0628 // Novo membro protegido declarado no tipo selado

    private readonly List<CandidateSkill> _skills = [];
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
    public IReadOnlyCollection<CandidateSkill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<string> Hobbies => _hobbies.AsReadOnly();
    public IReadOnlyCollection<Certificate> Certificates => _certificates.AsReadOnly();
    public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();
    public IReadOnlyCollection<WorkplaceType> DesiredWorkplaceTypes => _desiredWorkplaceTypes.AsReadOnly();
    public IReadOnlyCollection<JobType> DesiredJobTypes => _desiredJobTypes.AsReadOnly();

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
            return Error.Displayable("candidate", "invalid resume url");

        ResumeUrl = resumeUrl;

        return Result.Ok();
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

    public void ClearHobbies() => _hobbies.Clear();

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
        if (_skills.FirstOrDefault(s => s.Id == candidateLanguageSkillId) is not CandidateLanguageSkill skill)
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

    public Result AddDesiredJobType(JobType jobType)
    {
        if (_desiredJobTypes.Contains(jobType))
            return Error.Displayable("candidate_desired_job_type", "job type already added");

        _desiredJobTypes.Add(jobType);

        return Result.Ok();
    }

    public void ClearDesiredJobTypes() => _desiredJobTypes.Clear();

    public Result AddDesiredWorkplaceType(WorkplaceType workplaceType)
    {
        if (_desiredWorkplaceTypes.Contains(workplaceType))
            return Error.Displayable("candidate_desired_workplace_type", "workplace type already added");

        _desiredWorkplaceTypes.Add(workplaceType);
        return Result.Ok();
    }

    public void ClearDesiredWorkplaceTypes() => _desiredWorkplaceTypes.Clear();

    public Result UpdateProfilePicture(string profilePictureUrl)
    {
        if (!Uri.IsWellFormedUriString(profilePictureUrl, UriKind.Absolute))
            return Error.Displayable("candidate_profile_picture", "invalid profile picture url");

        ProfilePictureUrl = profilePictureUrl;
        return Result.Ok();
    }

    public Result UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Displayable("candidate_name", "invalid name");

        Name = name;
        return Result.Ok();
    }

    public Result UpdatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 11)
            return Error.Displayable("candidate_phone", "invalid candidate phone");

        Phone = phone;
        RaiseEvent(new CandidatePhoneUpdatedEvent(Id));

        return Result.Ok();
    }

    public Result UpdateAddress(Address address)
    {
        if (
            typeof(Address)
            .GetType()
            .GetProperties()
            .Any(p => string.IsNullOrWhiteSpace((string)p.GetValue(address)!))
        ) return Error.Displayable("candidate_address", "invalid address");

        Address = address;
        return Result.Ok();
    }

    public Result UpdateInstagramUrl(string instagramUrl)
    {
        if (!Uri.IsWellFormedUriString(instagramUrl, UriKind.Absolute))
            return Error.Displayable("candidate_instagram_url", "invalid url");

        InstagramUrl = instagramUrl;
        return Result.Ok();
    }

    public Result UpdateLinkedinUrl(string linkedinUrl)
    {
        if (!Uri.IsWellFormedUriString(linkedinUrl, UriKind.Absolute))
            return Error.Displayable("candidate_linkedin_url", "invalid url");

        InstagramUrl = linkedinUrl;
        return Result.Ok();
    }

    public Result UpdateGithubUrl(string githubUrl)
    {
        if (!Uri.IsWellFormedUriString(githubUrl, UriKind.Absolute))
            return Error.Displayable("candidate_Github_url", "invalid url");

        InstagramUrl = githubUrl;
        return Result.Ok();
    }

    public Result UpdateSummary(string summary)
    {
        if(string.IsNullOrWhiteSpace(summary))
            return Error.Displayable("candidate_summary", "invalid summary");

        if(summary.Length is < 10 or > 500)
            return Error.Displayable("candidate_summary", "candidate summary length must be between 10 and 500");
    
        Summary = summary;

        return Result.Ok();
    }
}
