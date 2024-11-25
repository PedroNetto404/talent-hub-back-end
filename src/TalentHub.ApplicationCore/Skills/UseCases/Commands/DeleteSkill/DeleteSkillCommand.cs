namespace TalentHub.ApplicationCore.Skills.UseCases.Commands.DeleteSkill;

using TalentHub.ApplicationCore.Core.Abstractions;

public sealed record DeleteSkillCommand(Guid Id) : ICommand;
