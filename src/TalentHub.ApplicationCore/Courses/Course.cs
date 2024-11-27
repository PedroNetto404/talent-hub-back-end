using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Courses;

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
        if (string.IsNullOrWhiteSpace(name)) return new Error("course", "Name is required");

        return new Course(name);
    }

    private readonly List<string> _tags = [];
    private readonly List<Guid> _RelatedSkills = [];

    public string Name { get; private set; }
    public IReadOnlyList<string> Tags => _tags.AsReadOnly();
    public IReadOnlyList<Guid> RelatedSkills => _RelatedSkills.AsReadOnly();

    public void ClearTags() => _tags.Clear();

    public Result AddTag(string tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return new Error("tag", "Tag is required");

        if(_tags.Contains(tag))
            return new Error("tag", "Tag already exists");

        _tags.Add(tag);
        return Result.Ok();
    }

    public void ClearRelatedSkills() => _RelatedSkills.Clear();

    public Result AddRelatedSkill(Guid skillId) 
    {
        if (skillId == Guid.Empty)
            return new Error("course", "Skill id is required");

        if(_RelatedSkills.Contains(skillId))
            return new Error("course", "Skill already exists");

        _RelatedSkills.Add(skillId);

        return Result.Ok();
    }

    public Result ChangeName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return new Error("course", "invalid course name");

        Name = name;

        return Result.Ok();
    }
}