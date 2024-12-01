using Humanizer;
using TalentHub.ApplicationCore.Resources.Candidates.Entities;

namespace TalentHub.ApplicationCore.Resources.Candidates.Dtos;

public sealed record LanguageProficiencyDto(
    string Language,
    string WritingLevel,
    string SpeakingLevel,
    string ListeningLevel
)
{
    public static LanguageProficiencyDto FromEntity(LanguageProficiency languageProficiency) =>
        new(
            languageProficiency.Language.Name,
            languageProficiency.WritingLevel.ToString().Underscore(),
            languageProficiency.SpeakingLevel.ToString().Underscore(),
            languageProficiency.ListeningLevel.ToString().Underscore()
        );
}
