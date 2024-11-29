using TalentHub.ApplicationCore.Candidates.Dtos;
using TalentHub.ApplicationCore.Core.Abstractions;

namespace TalentHub.ApplicationCore.Candidates.UseCases.Queries.GetCandidateById;

public sealed record GetCandidateByIdQuery(Guid Id) : IQuery<CandidateDto>;
