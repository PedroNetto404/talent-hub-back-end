using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Update;

public sealed record UpdateUniversityCommand(
    Guid Id,
    string Name,
    string? SiteUrl
) : ICommand;
