using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Universities.Dtos;
using TalentHub.ApplicationCore.Universities.Specs;

namespace TalentHub.ApplicationCore.Universities.UseCases.Commands.Create;

public sealed class CreateUniversityCommandHandler(IRepository<University> universityRepository)
    : ICommandHandler<CreateUniversityCommand, UniversityDto>
{
    public async Task<Result<UniversityDto>> Handle(CreateUniversityCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await universityRepository.FirstOrDefaultAsync(
            new GetUniversityByNameSpec(request.Name),
            cancellationToken);
        if (existing is not null) return new Error("university", "University with this name already exists.");

        var universityResult = University.Create(request.Name, request.SiteUrl);
        if (universityResult.IsFail) return universityResult.Error;

        await universityRepository.AddAsync(universityResult.Value, cancellationToken);
        
        return UniversityDto.FromEntity(universityResult.Value);
    }
}