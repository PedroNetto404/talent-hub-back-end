using TalentHub.ApplicationCore.Candidates.Enums;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Skills.Enums;

namespace TalentHub.ApplicationCore.Candidates.Entities;

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

    private readonly Dictionary<LanguageSkillType, Proficiency> _specialProficiency = new()
    {
        [LanguageSkillType.Writing] = Proficiency.Beginner,
        [LanguageSkillType.Listening] = Proficiency.Beginner,
        [LanguageSkillType.Speaking] = Proficiency.Beginner
    };

    public Language Language { get; private set; }

    public IReadOnlyDictionary<LanguageSkillType, Proficiency> SpecialProficiences =>
        _specialProficiency.AsReadOnly();

    public void UpdateProficiency(LanguageSkillType type, Proficiency proficiency) =>
        _specialProficiency[type] = proficiency;
}