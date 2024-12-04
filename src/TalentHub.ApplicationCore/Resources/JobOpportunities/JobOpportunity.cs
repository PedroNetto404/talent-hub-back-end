using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.JobOpportunities.Enums;
using TalentHub.ApplicationCore.Resources.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.JobOpportunities;

public sealed class JobOpportunity : AuditableAggregateRoot
{
    private readonly List<Guid> _requiredSkillIds = [];
    private readonly List<string> _benefits = [];

    public string Title { get; private set; }
    public string Description { get; private set; }
    public JobOpportunityStatus Status { get; private set; }
    public decimal? Salary { get; private set; }
    public Guid CompanyId { get; private set; }
    public Guid? RecruiterId { get; private set; }
    public Address? Location { get; private set; }
    public JobType JobType { get; private set; }
    public WorkplaceType WorkplaceType { get; private set; }
    public DateOnly? ApplicationDeadline { get; private set; }
    public IReadOnlyList<Guid> RequiredSkillIds => _requiredSkillIds.AsReadOnly();
    public IReadOnlyList<string> Benefits => _benefits.AsReadOnly();

    private JobOpportunity(
        string title,
        string description,
        JobOpportunityStatus status,
        Guid companyId,
        JobType jobType,
        WorkplaceType workplaceType)
    {
        Title = title;
        Description = description;
        Status = status;
        CompanyId = companyId;
        JobType = jobType;
        WorkplaceType = workplaceType;
    }

    public static Result<JobOpportunity> Create(
        string title,
        string description,
        JobOpportunityStatus status,
        Guid companyId,
        JobType jobType,
        WorkplaceType workplaceType,
        decimal? salary,
        Guid? recruiterId,
        Address? location,
        DateOnly? applicationDeadline)
    {
        if (
            Result.FailEarly(
                () => Result.FailIfIsNullOrWhiteSpace(title, "invalid title"),
                () => Result.FailIfIsNullOrWhiteSpace(description, "invalid description"),
                () => Result.FailIf(companyId == Guid.Empty, "invalid company ID"),
                () => Result.FailIf(
                        applicationDeadline.HasValue && applicationDeadline < DateOnly.FromDateTime(DateTime.Now), 
                        "application deadline cannot be in the past"
                      ),
                () => Result.FailIf(salary.HasValue && salary <= 0, "salary must be greater than zero")
            ) is { IsFail: true, Error: var result }
        )
        {
            return result;
        }

        return Result.Ok(
            new JobOpportunity(
                title,
                description,
                status,
                companyId,
                jobType,
                workplaceType
            )
            {
                Salary = salary,
                RecruiterId = recruiterId,
                Location = location,
                ApplicationDeadline = applicationDeadline
            });
    }

    public Result AddRequiredSkill(Guid skillId)
    {
        if (_requiredSkillIds.Contains(skillId))
        {
            return new Error("job_opportunity_skill", $"Skill with ID '{skillId}' is already required.");
        }

        _requiredSkillIds.Add(skillId);
        return Result.Ok();
    }

    public Result RemoveRequiredSkill(Guid skillId)
    {
        if (!_requiredSkillIds.Remove(skillId))
        {
            return new Error("job_opportunity_skill", $"Skill with ID '{skillId}' not found.");
        }

        return Result.Ok();
    }

    public Result AddBenefit(string benefit)
    {
        if (_benefits.Contains(benefit))
        {
            return new Error("job_opportunity_benefit", $"Benefit '{benefit}' already exists.");
        }

        _benefits.Add(benefit);
        return Result.Ok();
    }

    public Result RemoveBenefit(string benefit)
    {
        if (!_benefits.Remove(benefit))
        {
            return new Error("job_opportunity_benefit", $"Benefit '{benefit}' not found.");
        }

        return Result.Ok();
    }

    public Result UpdateStatus(JobOpportunityStatus status)
    {
        Status = status;
        return Result.Ok();
    }

    public Result UpdateSalary(decimal? salary)
    {
        if (salary.HasValue && salary <= 0)
        {
            return new Error("job_opportunity_salary", "Salary must be greater than zero.");
        }

        Salary = salary;
        return Result.Ok();
    }

    public Result UpdateApplicationDeadline(DateOnly? deadline)
    {
        if (deadline.HasValue && deadline < DateOnly.FromDateTime(DateTime.Now))
        {
            return new Error("job_opportunity_deadline", "Application deadline cannot be in the past.");
        }

        ApplicationDeadline = deadline;
        return Result.Ok();
    }

    public Result UpdateLocation(Address? location)
    {
        Location = location;
        return Result.Ok();
    }
}
