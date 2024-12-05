using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Commands.Delete;

public sealed record DeleteCompanySectorCommand(Guid Id) : ICommand;
