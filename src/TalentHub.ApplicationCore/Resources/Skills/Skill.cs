using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Skills.Enums;

namespace TalentHub.ApplicationCore.Resources.Skills;

public sealed class Skill : AggregateRoot
{
    private Skill(string name, SkillType type) => (Name, Type) = (name, type);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS0628 // New protected member declared in sealed type
    protected Skill() { }
#pragma warning restore CS0628 // New protected member declared in sealed type
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public static Result<Skill> Create(string name, SkillType type)
    {
        if (Result.FailIfIsNullOrWhiteSpace(name, "skill name is required") is { IsFail: true, Error: var error })
        {
            return error;
        }

        return new Skill(name, type);
    }

    private readonly List<string> _tags = [];
    public string Name { get; private set; }
    public SkillType Type { get; private set; }
    public IReadOnlyCollection<string> Tags => _tags.AsReadOnly();

    public Result AddTag(string tag)
    {
        tag = tag.Trim();

        if (Result.FailIfIsNullOrWhiteSpace(tag, "tag is required") is { IsFail: true, Error: var emptyError })
        {
            return emptyError;
        }

        if (Result.FailIf(_tags.Contains(tag), "tag already exists") is { IsFail: true, Error: var containsError })
        {
            return containsError;
        }

        _tags.Add(tag);

        return Result.Ok();
    }

    public void ClearTags() => _tags.Clear();

    public Result UpdateName(string name)
    {
        if (Result.FailIfIsNullOrWhiteSpace(name, "name is required") is { IsFail: true, Error: var error })
        {
            return error;
        }
        
        Name = name;

        return Result.Ok();
    }
}
