using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateProfilePicture;

public sealed class UpdateCandidateProfilePictureCommandHandler(
    IRepository<Candidate> candidateRepository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCandidateProfilePictureCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateCandidateProfilePictureCommand request,
        CancellationToken cancellationToken)
    {
        Candidate? candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        string fileName = candidate.ProfilePictureFileName;

        await fileStorage.DeleteAsync(FileBucketNames.CandidateProfilePicture, fileName, cancellationToken);

        string fileUrl = await fileStorage.SaveAsync(
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
            })
        {
            return error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
