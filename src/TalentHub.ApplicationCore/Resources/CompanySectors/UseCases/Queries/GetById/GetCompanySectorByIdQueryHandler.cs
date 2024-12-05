using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetById;

public sealed class GetCompanySectorByIdQueryHandler(
    IRepository<CompanySector> repository
) : IQueryHandler<GetCompanySectorByIdQuery, CompanySectorDto>
{
    public async Task<Result<CompanySectorDto>> Handle(GetCompanySectorByIdQuery request, CancellationToken cancellationToken)
    {
        CompanySector? companySector = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (companySector is null)
        {
            return Error.NotFound("company_sector");
        }

        return CompanySectorDto.FromEntity(companySector);
    }
}
