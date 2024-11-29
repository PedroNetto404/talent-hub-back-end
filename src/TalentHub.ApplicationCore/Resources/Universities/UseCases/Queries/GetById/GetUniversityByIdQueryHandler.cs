using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Universities.Dtos;

namespace TalentHub.ApplicationCore.Universities.UseCases.Queries.GetById;

public sealed class GetUniversityByIdQueryHandler(
    IRepository<University> universityRepository) : IQueryHandler<GetUniversityByIdQuery, UniversityDto>
{
    public async Task<Result<UniversityDto>> Handle(GetUniversityByIdQuery request, CancellationToken cancellationToken)
    {
        var university = await universityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (university is null) return NotFoundError.Value;
        
        return UniversityDto.FromEntity(university);
    }
}