using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.RemovePresentationVideo;

public sealed record RemoveCompanyPresentationVideoCommand(Guid CompanyId) : ICommand<CompanyDto>;
