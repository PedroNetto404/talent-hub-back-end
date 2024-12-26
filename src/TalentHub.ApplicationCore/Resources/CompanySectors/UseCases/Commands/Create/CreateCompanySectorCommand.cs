using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Create;

public sealed record CreateCompanySectorCommand(
    string Name
) : ICommand<CompanySectorDto>;
