using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Update;

public sealed record UpdateUniversityCommand(
    Guid Id,
    string Name,
    string? SiteUrl
) : ICommand<UniversityDto>;
