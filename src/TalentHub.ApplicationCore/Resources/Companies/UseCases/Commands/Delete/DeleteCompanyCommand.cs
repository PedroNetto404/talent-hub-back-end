using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Delete;

public sealed record DeleteCompanyCommand(Guid CompanyId) : ICommand;
