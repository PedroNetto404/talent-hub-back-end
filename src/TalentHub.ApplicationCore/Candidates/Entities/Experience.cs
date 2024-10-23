using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.Entities;

public abstract class Experience : Entity
{
    private readonly List<string> _activities = [];

    public DatePeriod Start { get; private set; }
    public DatePeriod? End { get; private set; }
    public bool IsCurrent { get; private set; }
    public IReadOnlyCollection<string> Activities => _activities.AsReadOnly();

    public void ToggleCurrent() => IsCurrent = !IsCurrent;

    public Result SetEndDate(DatePeriod endDate)
    {
        if (endDate < Start)
            return Error.Displayable("experience", $"{endDate} must be greater than {Start}");

        End = endDate;
        return Result.Ok();
    }

    public Result AddActivity(string activity)
    {
        if (_activities.Contains(activity))
        {
            return Error.Displayable("experience", $"Activity '{activity}' already exists.");
        }

        _activities.Add(activity);
        return Result.Ok();
    }

    public void ClearActivities() =>
        _activities.Clear();
}
