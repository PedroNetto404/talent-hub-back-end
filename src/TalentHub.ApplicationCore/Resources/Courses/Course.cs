using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Resources.Courses;

public sealed class Course : AggregateRoot
{
#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected Course() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS0628 // New protected member declared in sealed type

    private Course(string name) => Name = name;

    public static Result<Course> Create(string name)
    {
        if (Result.FailIf(string.IsNullOrWhiteSpace(name), "name is required") is { IsFail: true, Error: var error })
        {
            return error;
        }

        return new Course(name);
    }

    private readonly List<string> _tags = [];
    private readonly List<Guid> _relatedSkills = [];

    public string Name { get; private set; }
    public IReadOnlyList<string> Tags => _tags.AsReadOnly();
    public IReadOnlyList<Guid> RelatedSkills => _relatedSkills.AsReadOnly();

    public void ClearTags() => _tags.Clear();

    public Result AddTag(string tag)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(string.IsNullOrWhiteSpace(tag), "tag is required"),
                () => Result.FailIf(_tags.Contains(tag), "tag already exists")
            )
            is { IsFail: true, Error: var error })
        {
            return error;
        }

        _tags.Add(tag);
        return Result.Ok();
    }

    public void ClearRelatedSkills() => _relatedSkills.Clear();

    public Result AddRelatedSkill(Guid skillId)
    {
        if (
            Result.FailEarly(
                () => Result.FailIf(skillId == Guid.Empty, "Skill id is required"),
                () => Result.FailIf(_relatedSkills.Contains(skillId), "Skill already exists"))
            is { IsFail: true, Error: var error })
        {
            return error;
        }

        _relatedSkills.Add(skillId);

        return Result.Ok();
    }

    public Result ChangeName(string name)
    {
        if (Result.FailIf(string.IsNullOrWhiteSpace(name), "name is required") is { IsFail: true, Error: var error })
        {
            return error;
        }

        Name = name;

        return Result.Ok();
    }
}
