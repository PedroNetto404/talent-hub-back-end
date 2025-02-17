using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Experiences;
using TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages;

namespace TalentHub.ApplicationCore.Resources.Candidates.Dtos;

public sealed record LanguageProficiencyDto(
    Guid Id,
    string Language,
    string WritingLevel,
    string SpeakingLevel,
    string ListeningLevel
)
{
    public static LanguageProficiencyDto FromEntity(LanguageProficiency languageProficiency) =>
        new(
            languageProficiency.Id,
            languageProficiency.Language.Name,
            languageProficiency.WritingLevel.ToString().Underscore(),
            languageProficiency.SpeakingLevel.ToString().Underscore(),
            languageProficiency.ListeningLevel.ToString().Underscore()
        );
}
