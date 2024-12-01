using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Universities.Dtos;

namespace TalentHub.ApplicationCore.Resources.Universities.UseCases.Queries.GetById;

public sealed record GetUniversityByIdQuery(Guid Id) : IQuery<UniversityDto>;
