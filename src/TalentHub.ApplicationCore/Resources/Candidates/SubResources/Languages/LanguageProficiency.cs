using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages;

public sealed class LanguageProficiency : VersionedEntity
{
    public LanguageProficiency(
        Language language,
        Proficiency writingLevel,
        Proficiency listeningLevel,
        Proficiency speakingLevel
    )
    {
        Language = language;
        WritingLevel = writingLevel;
        ListeningLevel = listeningLevel;
        SpeakingLevel = speakingLevel;
    }

    public Proficiency WritingLevel { get; internal set; }
    public Proficiency ListeningLevel { get; internal set; }
    public Proficiency SpeakingLevel { get; internal set; }
    public Language Language { get; private set; }


#pragma warning disable CS0628 // New protected member declared in sealed type
    protected LanguageProficiency() { }
#pragma warning restore CS0628 // New protected member declared in sealed type
}
