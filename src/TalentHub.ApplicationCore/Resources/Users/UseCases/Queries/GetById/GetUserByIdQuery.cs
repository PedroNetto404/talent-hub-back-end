using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Users.Dtos;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Queries.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserDto>;
