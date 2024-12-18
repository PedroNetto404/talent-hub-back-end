using System.Data;
using TalentHub.ApplicationCore.Core.Abstractions;
using TalentHub.ApplicationCore.Resources.Users.Dtos;

namespace TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.UpdateProfilePicture;

public sealed record UpdateProfilePictureCommand(
    Guid Id,
    Stream File,
    string ContentType
) : ICommand<UserDto>;
