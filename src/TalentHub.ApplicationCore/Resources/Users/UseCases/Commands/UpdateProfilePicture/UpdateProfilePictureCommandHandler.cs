using TalentHub.ApplicationCore.Constants;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Ports;
using TalentHub.ApplicationCore.Resources.Users.Dtos;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.UpdateProfilePicture;

public sealed class UpdateProfilePictureCommandHandler(
    IRepository<User> userRepository,
    IFileStorage fileStorage
) : ICommandHandler<UpdateProfilePictureCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(
        UpdateProfilePictureCommand request,
        CancellationToken cancellationToken
    )
    {
        User? user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            return Error.NotFound("user");
        }

        if (user.ProfilePictureUrl is not null)
        {
            await fileStorage.DeleteAsync(
                FileBucketNames.UserProfilePicture,
                user.ProfilePictureFileName,
                cancellationToken
            );
        }

        string pictureUrl = await fileStorage.SaveAsync(
            FileBucketNames.UserProfilePicture,
            request.File,
            $"{user.ProfilePictureFileName}.{request.ContentType.Split("/").Last()}",
            request.ContentType,
            cancellationToken);

        Result result = user.ChangeProfilePicture(pictureUrl);
        if (result.IsFail)
        {
            return result.Error;
        }

        await userRepository.UpdateAsync(user, cancellationToken);
        return UserDto.FromEntity(user);
    }
}
