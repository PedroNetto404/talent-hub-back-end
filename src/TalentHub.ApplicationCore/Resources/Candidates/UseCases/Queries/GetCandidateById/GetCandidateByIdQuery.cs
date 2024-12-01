using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Candidates.Dtos;

namespace TalentHub.ApplicationCore.Resources.Candidates.UseCases.Queries.GetCandidateById;

public sealed record GetCandidateByIdQuery(Guid Id) : IQuery<CandidateDto>;
