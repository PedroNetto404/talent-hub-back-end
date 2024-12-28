using FastEndpoints;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.UpdateProfilePicture;
using TalentHub.Presentation.Web.Extensions;

namespace TalentHub.Presentation.Web.Endpoints.Users.SubResouces.UpdateProfilePicture;

public sealed class UpdateProfilePictureEndpoint :
    Ep.Req<UpdateProfilePictureRequest>.Res<UserDto>
{
    public override void Configure()
    {
        Patch("{userId:guid}/profile-picture");
        AllowFileUploads();
        Description(ep =>
            ep.Accepts<UpdateProfilePictureRequest>()
             .Produces<UserDto>()
             .Produces(StatusCodes.Status404NotFound)
             .Produces(StatusCodes.Status400BadRequest));
        Validator<UpdateProfilePictureRequestValidator>();
        Group<UsersEndpointsGroup>();
        Version(1);
    }

    public override async Task HandleAsync(UpdateProfilePictureRequest req, CancellationToken ct)
    {
        using MemoryStream memoryStream = new();
        await req.File.CopyToAsync(memoryStream, ct);

        await this.HandleUseCaseAsync(
            new UpdateProfilePictureCommand(
                req.UserId,
                memoryStream,
                req.File.ContentType
            ),
            ct
        );
    }
}
