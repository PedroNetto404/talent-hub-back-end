using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.UpdateCandidateResume;

public sealed class UpdateCandidateResumeCommandHandler(
    IRepository<Candidate> candidateRepository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCandidateResumeCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateCandidateResumeCommand request,
        CancellationToken cancellationToken)
    {
        (
            Guid candidateId,
            Stream resumeFile
        ) = request;

        Candidate? candidate = await candidateRepository.GetByIdAsync(candidateId, cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (candidate.ResumeUrl is not null)
        {
            await fileStorage.DeleteAsync(
                FileBucketNames.CandidateResumes,
                candidate.ResumeFileName,
                cancellationToken);
        }

        string url = await fileStorage.SaveAsync(
            FileBucketNames.CandidateResumes,
            resumeFile,
            candidate.ResumeFileName,
            "application/pdf",
            cancellationToken);

        if(candidate.SetResumeUrl(url) is { IsFail: true, Error: var setResumeUrlError})
        {
            return setResumeUrlError;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
