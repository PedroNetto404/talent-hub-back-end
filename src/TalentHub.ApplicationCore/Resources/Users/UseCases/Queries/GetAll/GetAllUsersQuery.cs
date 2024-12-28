using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Queries.GetAll;

public sealed record GetAllUsersQuery(
    string? UsernameLike,
    string? EmailLike,
    string? Role,
    int Limit,
    int Offset,
    string? SortBy,
    SortOrder SortOrder
) : IQuery<PageResponse<UserDto>>;
