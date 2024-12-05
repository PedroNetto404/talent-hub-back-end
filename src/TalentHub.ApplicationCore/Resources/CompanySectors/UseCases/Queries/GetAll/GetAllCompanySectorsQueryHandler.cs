using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetAll;

public sealed class GetAllCompanySectorsQueryHandler(
    IRepository<CompanySector> repository
) : IQueryHandler<GetAllCompanySectorsQuery, PagedResponse>
{
    public async Task<Result<PagedResponse>> Handle(
        GetAllCompanySectorsQuery request,
        CancellationToken cancellationToken
    )
    {
        List<CompanySector> companySectors = await repository.ListAsync(
            additionalSpec: (query) =>
            {
                query.Paginate(request.Limit, request.Offset);
                
                if (!string.IsNullOrWhiteSpace(request.SortBy))
                {
                    query.Sort(request.SortBy, request.Ascending);
                }
            },
            cancellationToken: cancellationToken
        );

        int total = await repository.CountAsync(cancellationToken: cancellationToken);

        return new PagedResponse(
            new(companySectors.Count, total, request.Offset, request.Limit),
            companySectors.Select(CompanySectorDto.FromEntity)
        );
    }
}
