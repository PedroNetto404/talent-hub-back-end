using MediatR;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Universities.UseCases.Commands.Delete;

public sealed record DeleteUniversityCommand(
    Guid Id
) : ICommand;