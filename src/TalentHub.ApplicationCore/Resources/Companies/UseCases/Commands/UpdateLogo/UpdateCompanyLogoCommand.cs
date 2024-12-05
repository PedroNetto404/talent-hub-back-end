using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateLogo;

public sealed record UpdateCompanyLogoCommand(
    Guid CompanyId,
    Stream File,
    string FileContentType
) : ICommand<CompanyDto>;
