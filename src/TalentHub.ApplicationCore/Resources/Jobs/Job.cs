using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;

namespace TalentHub.ApplicationCore.Resources.Jobs;

public sealed class Job : AuditableAggregateRoot
{
    private readonly List<Guid> _requiredSkillIds = [];
    private readonly List<Guid> _benefitIds = [];
    private readonly List<string> _responsibilities = [];
    private readonly List<string> _requirements = [];
    private readonly List<string> _additionalInformation = [];
    
    public string Title { get; private set; }
    public Guid? HiredCandidateId { get; private set; }
    public string Description { get; private set; }
    public ProfessionalLevel Level { get; private set; }
    public Guid RequiredCourseId { get; private set; }
    public bool ExclusiveForPeopleWithDisabilities { get; private set; }
    public bool AlsoAvailableForPeopleWithDisabilities { get; private set; }
    public JobStatus Status { get; private set; }
    public decimal? MinimumSalary { get; private set; }
    public decimal? MaximumSalary { get; private set; }
    public Guid CompanyId { get; private set; }
    public JobType JobType { get; private set; }
    public WorkplaceType WorkplaceType { get; private set; }
    public DateOnly? Deadline { get; private set; }
    public IReadOnlyList<Guid> RequiredSkillIds => _requiredSkillIds.AsReadOnly();
    public IReadOnlyList<Guid> BenefitIds => _benefitIds.AsReadOnly();
    public IReadOnlyList<string> Responsibilities => _responsibilities.AsReadOnly();
    public IReadOnlyList<string> Requirements => _requirements.AsReadOnly();
    public IReadOnlyList<string> AdditionalInformation => _additionalInformation.AsReadOnly();

    private Job(
        string title,
        string description,
        JobStatus status,
        Guid companyId,
        JobType jobType,
        WorkplaceType workplaceType,
        decimal? minimumSalary,
        DateOnly? deadline,
        bool exclusiveForPeopleWithDisabilities, 
        bool alsoAvailableForPeopleWithDisabilities
        )
    {
        Title = title;
        Description = description;
        Status = status;
        CompanyId = companyId;
        JobType = jobType;
        WorkplaceType = workplaceType;
        MinimumSalary = minimumSalary;
        Deadline = deadline;
        ExclusiveForPeopleWithDisabilities = exclusiveForPeopleWithDisabilities;
        AlsoAvailableForPeopleWithDisabilities =
            ExclusiveForPeopleWithDisabilities || alsoAvailableForPeopleWithDisabilities;
    }

    public static Result<Job> Create(
        string title,
        string description,
        JobStatus status,
        Guid companyId,
        JobType jobType,
        WorkplaceType workplaceType,
        decimal? salary,
        DateOnly? deadline,
        bool exclusiveForPeopleWithDisabilities,
        bool alsoAvailableForPeopleWithDisabilities
        )
    {
        if (
            Result.FailEarly(
                () => Result.FailIfIsNullOrWhiteSpace(title, "Invalid title."),
                () => Result.FailIfIsNullOrWhiteSpace(description, "Invalid description."),
                () => Result.FailIf(companyId == Guid.Empty, "Invalid company ID."),
                () => Result.FailIf(
                    deadline.HasValue && deadline < DateOnly.FromDateTime(DateTime.Now),
                    "Application deadline cannot be in the past."
                ),
                () => Result.FailIf(salary.HasValue && salary <= 0, "Salary must be greater than zero.")
            ) is { IsFail: true, Error: var result }
        )
        {
            return result;
        }

        return new Job(
            title,
            description,
            status,
            companyId,
            jobType,
            workplaceType,
            salary,
            deadline,
            exclusiveForPeopleWithDisabilities,
            alsoAvailableForPeopleWithDisabilities
        );
    }

    public Result AddResponsibility(string responsibility)
    {
        if (_responsibilities.Contains(responsibility))
        {
            return new Error("job_responsibility", "Responsibility already exists.");
        }

        _responsibilities.Add(responsibility);
        return Result.Ok();
    }

    public Result RemoveResponsibility(string responsibility)
    {
        if (!_responsibilities.Remove(responsibility))
        {
            return new Error("job_responsibility", "Responsibility not found.");
        }

        return Result.Ok();
    }

    public Result AddRequirement(string requirement)
    {
        if (_requirements.Contains(requirement))
        {
            return new Error("job_requirement", "Requirement already exists.");
        }

        _requirements.Add(requirement);
        return Result.Ok();
    }

    public Result RemoveRequirement(string requirement)
    {
        if (!_requirements.Remove(requirement))
        {
            return new Error("job_requirement", "Requirement not found.");
        }

        return Result.Ok();
    }

    public Result AddAdditionalInformation(string information)
    {
        if (_additionalInformation.Contains(information))
        {
            return new Error("job_additional_information", "Additional information already exists.");
        }

        _additionalInformation.Add(information);
        return Result.Ok();
    }

    public Result RemoveAdditionalInformation(string information)
    {
        if (!_additionalInformation.Remove(information))
        {
            return new Error("job_additional_information", "Additional information not found.");
        }

        return Result.Ok();
    }

    public Result AddBenefit(Guid benefitId)
    {
        if (_benefitIds.Contains(benefitId))
        {
            return new Error("job_benefit", $"Benefit with ID '{benefitId}' already exists.");
        }

        _benefitIds.Add(benefitId);
        return Result.Ok();
    }

    public Result RemoveBenefit(Guid benefitId)
    {
        if (!_benefitIds.Remove(benefitId))
        {
            return new Error("job_benefit", $"Benefit with ID '{benefitId}' not found.");
        }

        return Result.Ok();
    }

    public Result AddRequiredSkill(Guid skillId)
    {
        if (_requiredSkillIds.Contains(skillId))
        {
            return new Error("job_required_skill", $"Skill with ID '{skillId}' is already required.");
        }

        _requiredSkillIds.Add(skillId);
        return Result.Ok();
    }

    public Result RemoveRequiredSkill(Guid skillId)
    {
        if (!_requiredSkillIds.Remove(skillId))
        {
            return new Error("job_required_skill", $"Skill with ID '{skillId}' not found.");
        }

        return Result.Ok();
    }

    public Result UpdateStatus(JobStatus newStatus)
    {
        if (Status == newStatus)
        {
            return new Error("job_status", "The new status is the same as the current status.");
        }

        Status = newStatus;
        return Result.Ok();
    }

    public Result UpdateSalary(decimal? newSalary)
    {
        if (newSalary.HasValue && newSalary <= 0)
        {
            return new Error("job_salary", "Salary must be greater than zero.");
        }

        MinimumSalary = newSalary;
        return Result.Ok();
    }

    public Result UpdateDeadline(DateOnly? newDeadline)
    {
        if (newDeadline.HasValue && newDeadline < DateOnly.FromDateTime(DateTime.Now))
        {
            return new Error("job_deadline", "Deadline cannot be in the past.");
        }

        Deadline = newDeadline;
        return Result.Ok();
    }
}
