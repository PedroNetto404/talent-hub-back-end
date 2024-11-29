using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Universities.Dtos;

namespace TalentHub.ApplicationCore.Universities.UseCases.Queries.GetById;

public sealed record GetUniversityByIdQuery(Guid Id) : IQuery<UniversityDto>;