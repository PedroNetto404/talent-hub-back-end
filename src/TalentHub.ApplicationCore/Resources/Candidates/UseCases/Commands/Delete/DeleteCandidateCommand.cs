using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Commands.Delete;

public sealed record DeleteCandidateCommand(Guid CandidateId) : ICommand;
