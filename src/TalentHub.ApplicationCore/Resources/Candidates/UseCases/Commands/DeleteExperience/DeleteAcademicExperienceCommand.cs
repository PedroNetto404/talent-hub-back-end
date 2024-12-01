
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.DeleteExperience;

public sealed record DeleteExperienceCommand(
    Guid CandidateId,
    Guid ExperienceId
) : ICommand;

