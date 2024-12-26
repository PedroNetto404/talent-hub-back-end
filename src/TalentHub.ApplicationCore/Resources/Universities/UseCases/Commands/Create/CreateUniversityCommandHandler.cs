using Ardalis.Specification;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;
using TalentHub.ApplicationCore.Resources.Universities.Specs;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Commands.Create;

public sealed class CreateUniversityCommandHandler(IRepository<University> universityRepository)
    : ICommandHandler<CreateUniversityCommand, UniversityDto>
{
    public async Task<Result<UniversityDto>> Handle(CreateUniversityCommand request,
        CancellationToken cancellationToken)
    {
        University? existing = await universityRepository.FirstOrDefaultAsync(
            new GetUniversityByNameSpec(request.Name),
            cancellationToken);
        if (existing is not null)
        {
            return Error.InvalidInput("university with this name already exists");
        }

        Result<University> universityResult = University.Create(request.Name, request.SiteUrl);
        if (universityResult.IsFail)
        {
            return universityResult.Error;
        }

        await universityRepository.AddAsync(universityResult.Value, cancellationToken);

        return UniversityDto.FromEntity(universityResult.Value);
    }
}
