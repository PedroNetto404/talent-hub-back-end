using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemoveGaleryItem;

public sealed record RemoveCompanyGaleryItemCommand(
    Guid CompanyId,
    string Url
) : ICommand<CompanyDto>;
