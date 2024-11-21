using System.Windows.Input;
using MediatR;
using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Jobs.Enums;
using TalentHub.ApplicationCore.Shared.ValueObjects;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Commands.CreateCandidate;

public sealed record CreateCandidateCommand(
    string Name,
    string Email,
    string Phone,
    DateOnly BirthDate,
    Address Address,
    JobType[] DesiredJobTypes,
    WorkplaceType[] DesiredWorkplaceTypes,
    string? Summary,
    string? GithubUrl,
    string? InstagramUrl,
    string? LinkedinUrl,
    decimal? ExpectedRemuneration,
    string[] Hobbies
) : ICommand<CandidateDto>;