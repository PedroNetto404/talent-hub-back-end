using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Skills.UseCases.Dtos;

namespace TalentHub.ApplicationCore.Skills.UseCases.Commands.CreateSkill;

public sealed record CreateSkillCommand(
    string Name,
    string Type, 
    string[] Tags,
    bool IsSuggestion
) : ICommand<SkillDto>;