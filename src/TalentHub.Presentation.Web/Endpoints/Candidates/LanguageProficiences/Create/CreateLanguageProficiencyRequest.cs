namespace TalentHub.Presentation.Web.Endpoints.Candidates.LanguageProficiences.Shared;

public sealed record CreateLanguageProficiencesRequest(
    string Language,
    string WritingLevel,
    string ListeningLevel,
    string SpeakingLevel
);
