using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdateGaleryItem;

public sealed record UpdateCompanyGaleryCommand(
    Guid CompanyId,
    Stream File,
    string FileContentType
) : ICommand<CompanyDto>;
