using Humanizer;
using TalentHub.ApplicationCore.Core.Results;

namespace TalentHub.ApplicationCore.Skills.UseCases.Dtos;

public sealed record SkillDto(
    Guid Id,
    string Name,
    string Type,
    IEnumerable<string> Tags
)
{
    public static SkillDto FromEntity(Skill skill) =>
        new(
            skill.Id,
            skill.Name,
            skill.Type.ToString().Underscore(),
            skill.Tags
        );
}

