using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Delete;

public sealed record DeleteUniversityCommand(
    Guid Id
) : ICommand;
