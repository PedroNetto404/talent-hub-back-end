using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.ApplicationCore.Resources.CompanySectors.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Specs;

namespace TalentHub.ApplicationCore.Resources.CompanySectors.UseCases.Queries.GetAll;

public sealed class GetAllCompanySectorsQueryHandler(
    IRepository<CompanySector> repository
) : IQueryHandler<GetAllCompanySectorsQuery, PageResponse>
{
    public async Task<Result<PageResponse>> Handle(
        GetAllCompanySectorsQuery request,
        CancellationToken cancellationToken
    )
    {
        List<CompanySector> companySectors = await repository.ListAsync(
            new GetPageSpec<CompanySector>(request.Limit, request.Offset, request.SortBy, request.SortOrder),
            cancellationToken: cancellationToken
        );

        int total = await repository.CountAsync(cancellationToken: cancellationToken);

        return new PageResponse(
            new(companySectors.Count, total, request.Offset, request.Limit),
            companySectors.Select(CompanySectorDto.FromEntity)
        );
    }
}
