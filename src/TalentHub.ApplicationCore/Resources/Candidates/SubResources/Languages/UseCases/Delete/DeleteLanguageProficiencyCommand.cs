using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.SubResources.Languages.UseCases.Delete;

public sealed record DeleteLanguageProficiencyCommand(
    Guid CandidateId,
    Guid LanguageProficiencyId
) : ICommand;
