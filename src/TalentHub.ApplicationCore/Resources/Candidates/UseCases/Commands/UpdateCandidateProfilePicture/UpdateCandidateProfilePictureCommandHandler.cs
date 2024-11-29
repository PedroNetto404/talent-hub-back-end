using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateProfilePicture;

public sealed class UpdateCandidateProfilePictureCommandHandler(
    IRepository<Candidate> candidateRepository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCandidateProfilePictureCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateCandidateProfilePictureCommand request,
        CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null) return NotFoundError.Value;

        var fileName = candidate.ProfilePictureFileName;

        await fileStorage.DeleteAsync(FileBucketNames.CandidateProfilePicture, fileName, cancellationToken);

        var fileUrl = await fileStorage.SaveAsync(
            FileBucketNames.CandidateProfilePicture,
            request.File,
            $"{fileName}.{request.ContentType.Split("/").Last()}",
            request.ContentType,
            cancellationToken
        );

        if (candidate.UpdateProfilePicture(fileUrl) is
            {
                IsFail: true,
                Error: var error
            }) return error;

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}