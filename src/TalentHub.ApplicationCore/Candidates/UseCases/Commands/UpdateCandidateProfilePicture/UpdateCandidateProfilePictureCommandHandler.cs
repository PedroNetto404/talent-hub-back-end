using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateProfilePicture;

public sealed class UpdateCandidateProfilePictureCommandHandler(
    IRepository<Candidate> repository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCandidateProfilePictureCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateCandidateProfilePictureCommand request, 
        CancellationToken cancellationToken)
    {
        var candidate = await repository.GetByIdAsync(request.CandidateId, cancellationToken);
        if(candidate is null) return Error.Displayable("not_found", "candidate not found");

        var fileName = candidate.ProfilePictureFileName;

        await fileStorage.DeleteAsync(fileName, cancellationToken);

        var fileUrl = await fileStorage.SaveAsync(
            request.File,
            fileName,
            request.ContentType,
            cancellationToken
        );

        if(candidate.UpdateProfilePicture(fileUrl) is 
        {
            IsFail: true,
            Error: var error
        }) return error;

        await repository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}