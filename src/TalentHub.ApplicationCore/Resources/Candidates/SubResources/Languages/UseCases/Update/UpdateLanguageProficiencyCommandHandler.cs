using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Update;

public sealed class UpdateLanguageProficiencyCommandHandler(
    IRepository<Candidate> candidateRepository
) : ICommandHandler<UpdateLanguageProficiencyCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(
        UpdateLanguageProficiencyCommand request,
        CancellationToken cancellationToken
    )
    {
        Candidate? candidate = await candidateRepository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId), cancellationToken);
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (!Enum.TryParse(request.WritingLevel, true, out Proficiency writingLevel))
        {
            return Error.InvalidInput($"{request.WritingLevel} is not valid proficiency");
        }

        if (!Enum.TryParse(request.SpeakingLevel, true, out Proficiency speakingLevel))
        {
            return Error.InvalidInput($"{request.SpeakingLevel} is not valid proficiency");
        }

        if (!Enum.TryParse(request.ListeningLevel, true, out Proficiency listeningLevel))
        {
            return Error.InvalidInput($"{request.ListeningLevel} is not valid proficiency");
        }

        Result updateResult = candidate.UpdateLanguageProficiency(
            request.LanguageProficiencyId,
            writingLevel,
            speakingLevel,
            listeningLevel
        );
        if (updateResult.IsFail)
        {
            return updateResult.Error;
        }

        await candidateRepository.UpdateAsync(candidate, cancellationToken);
        return CandidateDto.FromEntity(candidate);
    }
}
