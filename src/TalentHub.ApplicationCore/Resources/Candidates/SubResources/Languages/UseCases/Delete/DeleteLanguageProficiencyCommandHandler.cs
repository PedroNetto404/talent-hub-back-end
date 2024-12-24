using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Delete;

public sealed class DeleteLanguageProficiencyCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<DeleteLanguageProficiencyCommand>
{
    public async Task<Result> Handle(
        DeleteLanguageProficiencyCommand request,
        CancellationToken cancellationToken
    )
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (candidate.RemoveLanguage(request.LanguageProficiencyId) is
            {
                IsFail: true,
                Error: var error
            })
        {
            return error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return Result.Ok();
    }
}
