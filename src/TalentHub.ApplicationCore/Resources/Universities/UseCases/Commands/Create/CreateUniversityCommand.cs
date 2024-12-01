using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Create;

public sealed record CreateUniversityCommand(
    string Name,
    string? SiteUrl
) : ICommand<UniversityDto>;
