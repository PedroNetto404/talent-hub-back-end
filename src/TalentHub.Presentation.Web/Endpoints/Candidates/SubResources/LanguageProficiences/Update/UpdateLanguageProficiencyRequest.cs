namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.LanguageProficiences.Update;

public sealed record UpdateLanguageProficiencyRequest(
    string WritingLevel,
    string ListeningLevel,
    string SpeakingLevel
);
