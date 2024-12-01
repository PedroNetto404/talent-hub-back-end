using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Skills.UseCases.Commands.DeleteSkill;

public sealed record DeleteSkillCommand(Guid Id) : ICommand;
