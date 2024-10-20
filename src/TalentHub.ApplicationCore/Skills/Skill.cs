using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.ApplicationCore.Skills;

public sealed class Skill(string name, SkillType type) : AggregateRoot
{
    private readonly List<string> _tags = [];

    public string Name { get; private set; } = name;
    public SkillType Type { get; private set; } = type;
    public IReadOnlyCollection<string> Tags => _tags.AsReadOnly();
    public bool Approved { get; private set; } = false;
    public bool IsSuggestion => !Approved;

    public void Approve() => Approved = true;

    public Result AddTag(string tag) 
    {
        tag = tag.Trim();

        if(string.IsNullOrWhiteSpace(tag)) 
        {
            return Error.Displayable("skill", "skill tag cannot be empty");
        }

        if(_tags.Contains(tag))
        {
            return Error.Displayable("skill", $"skill tag already contains {tag}");
        }

        _tags.Add(tag);

        return Result.Ok();
    }

    public Result RemoveTag(string tag)
    {
        tag = tag.Trim();

        if(!tag.Contains(tag))
        {
            return Error.Displayable("skill", $"{tag} not exists");
        }

        _tags.Remove(tag);

        return Result.Ok();
    }
}