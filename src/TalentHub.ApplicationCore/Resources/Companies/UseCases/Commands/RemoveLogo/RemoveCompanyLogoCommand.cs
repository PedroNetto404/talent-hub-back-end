using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveLogo;

public sealed record RemoveCompanyLogoCommand(Guid CompanyId) : ICommand<CompanyDto>;
