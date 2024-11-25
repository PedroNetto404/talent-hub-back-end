using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Skills.UseCases.Commands.UpdateSkill;

public sealed record UpdateSkillCommand(
    Guid Id,
    string Name,
    IEnumerable<string> Tags
) : ICommand;
