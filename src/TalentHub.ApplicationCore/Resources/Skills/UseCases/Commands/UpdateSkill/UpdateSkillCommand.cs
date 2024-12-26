using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.UpdateSkill;

public sealed record UpdateSkillCommand(
    Guid SkillId,
    string Name,
    string Type,
    IEnumerable<string> Tags
) : ICommand;
