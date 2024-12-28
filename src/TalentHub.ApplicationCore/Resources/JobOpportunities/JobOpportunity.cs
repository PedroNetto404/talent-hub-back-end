using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.JobOpportunities.Enums;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.JobOpportunities;

public sealed class JobOpportunity : AuditableAggregateRoot
{
    private readonly List<Guid> _requiredSkills = [];
    private readonly List<Guid> _benefits = [];
    private readonly List<string> _responsibilities = [];
    private readonly List<string> _requirements = [];
    private readonly List<string> _additionalInformation = [];

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid? HiredCandidateId { get; private set; }
    public ProfessionalLevel Level { get; private set; }
    public Address? Location { get; private set; }
    public Guid RequiredCourseId { get; private set; }
    public bool ExclusiveForPeopleWithDisabilities { get; private set; }
    public bool AlsoAvailableForPeopleWithDisabilities { get; private set; }
    public JobStatus Status { get; private set; }
    public decimal? Remuneration { get; private set; }
    public Guid CompanyId { get; private set; }
    public JobType JobType { get; private set; }
    public WorkplaceType WorkplaceType { get; private set; }
    public DateOnly? Deadline { get; private set; }
    public IReadOnlyList<Guid> RequiredSkills =>
        _requiredSkills.AsReadOnly();
    public IReadOnlyList<Guid> Benefits =>
        _benefits.AsReadOnly();
    public IReadOnlyList<string> Responsibilities =>
        _responsibilities.AsReadOnly();
    public IReadOnlyList<string> Requirements =>
        _requirements.AsReadOnly();
    public IReadOnlyList<string> AdditionalInformation =>
        _additionalInformation.AsReadOnly();

    private JobOpportunity(
        string title,
        string description,
        JobStatus status,
        Guid companyId,
        JobType jobType,
        WorkplaceType workplaceType,
        decimal? remuneration,
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
        Remuneration = remuneration;
        Deadline = deadline;
        ExclusiveForPeopleWithDisabilities =
            exclusiveForPeopleWithDisabilities;
        AlsoAvailableForPeopleWithDisabilities =
            ExclusiveForPeopleWithDisabilities ||
            alsoAvailableForPeopleWithDisabilities;
    }

    public static Result<JobOpportunity> Create(
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
                () => Result.FailIf(string.IsNullOrWhiteSpace(title), "Invalid title."),
                () => Result.FailIf(string.IsNullOrWhiteSpace(description), "Invalid description."),
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

        return new JobOpportunity(
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
            return Error.InvalidInput("Responsibility already exists.");
        }

        _responsibilities.Add(responsibility);
        return Result.Ok();
    }

    public Result RemoveResponsibility(string responsibility)
    {
        if (!_responsibilities.Remove(responsibility))
        {
            return Error.InvalidInput("Responsibility not found.");
        }

        return Result.Ok();
    }

    public Result AddRequirement(string requirement)
    {
        if (_requirements.Contains(requirement))
        {
            return Error.InvalidInput("Requirement already exists.");
        }

        _requirements.Add(requirement);
        return Result.Ok();
    }

    public Result RemoveRequirement(string requirement)
    {
        if (!_requirements.Remove(requirement))
        {
            return Error.InvalidInput("Requirement not found.");
        }

        return Result.Ok();
    }

    public Result AddAdditionalInformation(string information)
    {
        if (_additionalInformation.Contains(information))
        {
            return Error.InvalidInput("Additional information already exists.");
        }

        _additionalInformation.Add(information);
        return Result.Ok();
    }

    public Result RemoveAdditionalInformation(string information)
    {
        if (!_additionalInformation.Remove(information))
        {
            return Error.InvalidInput("Additional information not found.");
        }

        return Result.Ok();
    }

    public Result AddBenefit(Guid benefitId)
    {
        if (_benefits.Contains(benefitId))
        {
            return Error.InvalidInput("Benefit already exists.");
        }

        _benefits.Add(benefitId);
        return Result.Ok();
    }

    public Result RemoveBenefit(Guid benefitId)
    {
        if (!_benefits.Remove(benefitId))
        {
            return Error.InvalidInput("Benefit not found.");
        }

        return Result.Ok();
    }

    public Result AddRequiredSkill(Guid skillId)
    {
        if (_requiredSkills.Contains(skillId))
        {
            return Error.InvalidInput("Required skill already exists.");
        }

        _requiredSkills.Add(skillId);
        return Result.Ok();
    }

    public Result RemoveRequiredSkill(Guid skillId)
    {
        if (!_requiredSkills.Remove(skillId))
        {
            return Error.InvalidInput("Skill not found.");
        }

        return Result.Ok();
    }

    public Result UpdateStatus(JobStatus newStatus)
    {
        if (Status == newStatus)
        {
            return Error.InvalidInput("The new status is the same as the current status.");
        }

        Status = newStatus;
        return Result.Ok();
    }

    public Result UpdateRemuneration(decimal? remuneration)
    {
        if (remuneration.HasValue && remuneration <= 0)
        {
            return Error.InvalidInput("Salary must be greater than zero.");
        }

        Remuneration = remuneration;
        return Result.Ok();
    }

    public Result UpdateDeadline(DateOnly? newDeadline)
    {
        if (newDeadline.HasValue && newDeadline < DateOnly.FromDateTime(DateTime.Now))
        {
            return Error.InvalidInput("Deadline cannot be in the past.");
        }

        Deadline = newDeadline;
        return Result.Ok();
    }
}
