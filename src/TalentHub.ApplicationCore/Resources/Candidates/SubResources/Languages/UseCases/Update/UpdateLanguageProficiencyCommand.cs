using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Update;

public sealed record UpdateLanguageProficiencyCommand(
    Guid CandidateId,
    Guid LanguageProficiencyId,
    string WritingLevel,
    string ListeningLevel,
    string SpeakingLevel
) : ICommand<CandidateDto>;
