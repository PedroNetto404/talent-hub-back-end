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
        if (string.IsNullOrWhiteSpace(name))
            return new Error("skill", "skill name cannot be blank.");

        return new Skill(name, type);
    }

    private readonly List<string> _tags = [];
    public string Name { get; private set; } 
    public SkillType Type { get; private set; } 
    public IReadOnlyCollection<string> Tags => _tags.AsReadOnly();

    public Result AddTag(string tag)
    {
        tag = tag.Trim();

        if (string.IsNullOrWhiteSpace(tag))
        {
            return new Error("skill", "skill tag cannot be empty");
        }

        if (_tags.Contains(tag))
        {
            return new Error("skill", $"skill tag already contains {tag}");
        }

        _tags.Add(tag);

        return Result.Ok();
    }

    public void ClearTags() => _tags.Clear();

    public Result UpdateName(string name)
    {
        if(string.IsNullOrWhiteSpace(name)) return new Error("skill", "skill name cannot be blank"); 

        Name = name;

        return Result.Ok();
    }
}
