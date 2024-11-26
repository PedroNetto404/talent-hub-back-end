using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.UpdateCandidateResume;

public sealed class UpdateCandidateResumeCommandHandler(
    IRepository<Candidate> candidateRepository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateCandidateResumeCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateCandidateResumeCommand request,
        CancellationToken cancellationToken)
    {
        var candidate = await candidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (candidate is null)
            return NotFoundError.Value;

        if (candidate.ResumeUrl is not null)
            await fileStorage.DeleteAsync(
                FileBucketNames.CandidateResumes,
                candidate.ResumeFileName,
                cancellationToken);

        var url = await fileStorage.SaveAsync(
            FileBucketNames.CandidateResumes,
            request.File,
            candidate.ResumeFileName,
            "application/pdf",
            cancellationToken);

        var result = candidate.SetResumeUrl(url);
        if (result.IsFail) return result.Error;

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}