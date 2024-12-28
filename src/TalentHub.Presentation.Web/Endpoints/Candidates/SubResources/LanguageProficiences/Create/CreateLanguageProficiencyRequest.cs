namespace TalentHub.Presentation.Web.Endpoints.Candidates.SubResources.LanguageProficiences.Create;

public sealed record CreateLanguageProficiencesRequest(
    string Language,
    string WritingLevel,
    string ListeningLevel,
    string SpeakingLevel
);
