using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Commands.Create;

public sealed record CreateCompanyCommand(
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
    string? FacebookUrl,
    string? LinkedinUrl,
    string? CareerPageUrl,
    string? Mission,
    string? Vision,
    string? Values,
    int FoundationYear
) : ICommand<CompanyDto>;
