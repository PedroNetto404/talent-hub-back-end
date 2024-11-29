using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.DeleteCandidate;

public sealed record DeleteCandidateCommand(Guid CandidateId) : ICommand;
