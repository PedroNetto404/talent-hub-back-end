using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Create;

public sealed record CreateLanguageProficiencyCommand(
    Guid CandidateId,
    string WritingLevel,
    string ListeningLevel,
    string SpeakingLevel,
    string Language
) : ICommand<CandidateDto>;
