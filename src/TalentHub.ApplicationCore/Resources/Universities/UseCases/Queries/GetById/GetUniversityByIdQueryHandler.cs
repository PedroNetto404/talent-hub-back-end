using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetById;

public sealed class GetUniversityByIdQueryHandler(
    IRepository<University> universityRepository) : IQueryHandler<GetUniversityByIdQuery, UniversityDto>
{
    public async Task<Result<UniversityDto>> Handle(GetUniversityByIdQuery request, CancellationToken cancellationToken)
    {
        University? university = await universityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (university is null) 
        {
            return Error.NotFound("university");
        }
        
        return UniversityDto.FromEntity(university);
    }
}
