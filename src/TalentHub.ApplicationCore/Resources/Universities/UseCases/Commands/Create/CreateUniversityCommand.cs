using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Universities.Dtos;

namespace TalentHub.ApplicationCore.Universities.UseCases.Commands.Create;

public sealed record CreateUniversityCommand(
    string Name,
    string? SiteUrl
) : ICommand<UniversityDto>;