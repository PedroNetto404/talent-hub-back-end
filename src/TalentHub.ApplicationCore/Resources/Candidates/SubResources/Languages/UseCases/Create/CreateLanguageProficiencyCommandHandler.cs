using Humanizer;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;
using TalentHub.ApplicationCore.Resources.Candidates.Specs;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Create;

public sealed class CreateLanguageProficiencyCommandHandler(
    IRepository<Candidate> repository
) :
    ICommandHandler<CreateLanguageProficiencyCommand, CandidateDto>
{
    public async Task<Result<CandidateDto>> Handle(CreateLanguageProficiencyCommand request, CancellationToken cancellationToken)
    {
        Candidate? candidate = await repository.FirstOrDefaultAsync(new GetCandidateByIdSpec(request.CandidateId));
        if (candidate is null)
        {
            return Error.NotFound("candidate");
        }

        if (!Enum.TryParse(request.WritingLevel.Pascalize(), true, out Proficiency writingLevel))
        {
            return Error.BadRequest($"{request.WritingLevel} is not valid proficiency");
        }

        if (!Enum.TryParse(request.SpeakingLevel.Pascalize(), true, out Proficiency speakingLevel))
        {
            return Error.BadRequest($"{request.SpeakingLevel} is not valid proficiency");
        }

        if (!Enum.TryParse(request.ListeningLevel.Pascalize(), true, out Proficiency listeningLevel))
        {
            return Error.BadRequest($"{request.ListeningLevel} is not valid proficiency");
        }

        if (!Language.TryFromName(request.Language, true, out Language language))
        {
            return Error.BadRequest($"{request.Language} is not valid language");
        }

        var languageProficiency = new LanguageProficiency(
            language,
            writingLevel,
            listeningLevel,
            speakingLevel
        );

        if (candidate.AddLanguage(languageProficiency) is { IsFail: true, Error: var error })
        {
            return error;
        }

        try
        {
            await repository.UpdateAsync(candidate, cancellationToken);
        }
        catch (System.Exception e)
        {
            throw;
        }

        return CandidateDto.FromEntity(candidate);
    }
}
