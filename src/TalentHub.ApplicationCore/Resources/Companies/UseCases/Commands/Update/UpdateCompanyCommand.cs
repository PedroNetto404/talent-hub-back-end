using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Update;

public sealed record UpdateCompanyCommand(
    Guid CompanyId,
    string LegalName,
    string TradeName,
    string Cnpj,
    string? About,
    Guid SectorId,
    string RecruitmentEmail,
    string? Phone,
    bool AutoMatchEnabled,
    int EmployeeCount,
    string? SiteUrl,
    Address Address,
    string? InstagramUrl,
    string? LinkedinUrl,
    string? CareerPageUrl,
    string? Mission,
    string? Vision,
    string? Values,
    int FoundantionYear
) : ICommand;
