using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.UpdatePresentationVideo;

public sealed record UpdateCompanyPresentationVideoCommand(
    Guid CompanyId,
    Stream File,
    string FileContentType
) : ICommand<CompanyDto>;
