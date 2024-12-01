using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Enums;

namespace TalentHub.ApplicationCore.Resources.Candidates.Entities;

public sealed class LanguageProficiency : Entity
{
    public LanguageProficiency(Language language) => Language = language;

#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected LanguageProficiency()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS0628 // New protected member declared in sealed type
    {
    }

    public Proficiency WritingLevel { get; private set; }
    public Proficiency ListeningLevel { get; private set; }
    public Proficiency SpeakingLevel { get; private set; }
    public Language Language { get; private set; }

    public void UpdateProficiency(LanguageSkillType type, Proficiency proficiency)
    {
        switch (type)
        {
            case LanguageSkillType.Writing:
                WritingLevel = proficiency;
                break;
            case LanguageSkillType.Listening:
                ListeningLevel = proficiency;
                break;
            case LanguageSkillType.Speaking:
                SpeakingLevel = proficiency;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
