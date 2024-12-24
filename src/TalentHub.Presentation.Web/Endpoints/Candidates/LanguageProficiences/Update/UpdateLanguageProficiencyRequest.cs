namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Update;

public sealed record UpdateLanguageProficiencyRequest(
    string WritingLevel,
    string ListeningLevel,
    string SpeakingLevel
);
