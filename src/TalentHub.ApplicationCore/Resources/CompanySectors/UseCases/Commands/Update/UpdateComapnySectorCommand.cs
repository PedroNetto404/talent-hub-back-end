using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Update;

public sealed record UpdateCompanySectorCommand(Guid Id, string Name) : ICommand<CompanySectorDto>;
