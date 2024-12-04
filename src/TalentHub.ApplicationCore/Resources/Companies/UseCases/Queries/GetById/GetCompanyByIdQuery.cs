using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Companies.Dtos;

namespace TalentHub.ApplicationCore.Resources.Companies.UseCases.Queries.GetById;

public sealed record GetCompanyByIdQuery(
    Guid CompanyId 
) : IQuery<CompanyDto>;