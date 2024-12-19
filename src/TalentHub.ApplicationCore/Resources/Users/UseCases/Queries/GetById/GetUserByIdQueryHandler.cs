using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users.Dtos;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Queries.GetById;

public sealed class GetUserByIdQueryHandler(
    IRepository<User> userRepository
) : IQueryHandler<GetUserByIdQuery, UserDto>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Error.NotFound("user");
        }

        return UserDto.FromEntity(user);
    }
}
