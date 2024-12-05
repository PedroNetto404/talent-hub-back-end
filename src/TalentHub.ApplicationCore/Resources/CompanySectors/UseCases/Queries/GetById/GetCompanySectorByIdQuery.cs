using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetById;

public sealed record GetCompanySectorByIdQuery(Guid Id) : ICachedQuery<CompanySectorDto>
{
    public TimeSpan? Duration => TimeSpan.FromHours(12);

    public string Key => $"{nameof(GetCompanySectorByIdQuery)}-{Id}";
}
