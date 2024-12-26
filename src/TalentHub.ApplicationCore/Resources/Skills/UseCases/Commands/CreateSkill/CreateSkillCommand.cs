using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Skills.Dtos;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.CreateSkill;

public sealed record CreateSkillCommand(
    string Name,
    string Type,
    IEnumerable<string> Tags
) : ICommand<SkillDto>;
