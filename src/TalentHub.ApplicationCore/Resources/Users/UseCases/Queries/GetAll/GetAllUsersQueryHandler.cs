using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.Enums;
using TalentHub.ApplicationCore.Resources.Users.Specs;
using TalentHub.ApplicationCore.Shared.Dtos;
using TalentHub.ApplicationCore.Shared.Enums;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Queries.GetAll;

public sealed class GetAllUsersQueryHandler(
    IRepository<User> userRepository
) : IQueryHandler<GetAllUsersQuery, PageResponse<UserDto>>
{
    public async Task<Result<PageResponse<UserDto>>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!Role.TryFromName(request.Role, true, out Role? role))
        {
            return Error.InvalidInput("invalid role");
        }

        List<User> users = await userRepository.ListAsync(
            new GetUsersSpec(
                request.UsernameLike,
                request.EmailLike,
                role,
                request.Limit,
                request.Offset,
                request.SortBy,
                request.SortOrder
            ),
            cancellationToken
        );

        int count = await userRepository.CountAsync(
            new GetUsersSpec(
                request.UsernameLike,
                request.EmailLike,
                role,
                int.MaxValue,
                0,
                "id",
                SortOrder.Ascending
            ),
            cancellationToken
        );


        UserDto[] dtos = [.. users.Select(UserDto.FromEntity)];
        return new PageResponse<UserDto>(
            new Meta(dtos.Length, count, request.Offset, request.Limit),
            dtos
        );
    }
}
