using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Certificates.UseCases.Commands;

public abstract class Experience : VersionedEntity
{
    protected Experience(
        DatePeriod start,
        DatePeriod? end,
        bool isCurrent)
    {
        Start = start;
        End = end;
        IsCurrent = isCurrent;
    }

    private readonly List<string> _activities = [];

    public DatePeriod Start { get; private set; }
    public DatePeriod? End { get; private set; }
    public bool IsCurrent { get; private set; }
    public IReadOnlyCollection<string> Activities => _activities.AsReadOnly();

    public void SetIsCurrent(bool isCurrent) =>
        IsCurrent = isCurrent;

    
    public Result UpdateDateRange(DatePeriod start, DatePeriod? end)
    {
        if (end != null && start > end)
        {
            return new Error("experience", "Start date must be less than end date.");
        }

        Start = start;
        End = end;
        
        return Result.Ok();
    }

    public Result AddActivity(string activity)
    {
        if (_activities.Contains(activity))
        {
            return new Error("experience", $"Activity '{activity}' already exists.");
        }

        _activities.Add(activity);
        return Result.Ok();
    }

    public void ClearActivities() =>
        _activities.Clear();

    //Required By EF Core
    protected Experience() { }
}
