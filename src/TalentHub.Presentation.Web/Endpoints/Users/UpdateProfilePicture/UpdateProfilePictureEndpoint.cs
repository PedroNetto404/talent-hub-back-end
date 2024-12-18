using System.Data;
using FastEndpoints;
using MediatR;
using TalentHub.ApplicationCore.Core.Results;
using TalentHub.ApplicationCore.Resources.Users.Dtos;
using TalentHub.ApplicationCore.Resources.Users.UseCases.Commands.UpdateProfilePicture;
using TalentHub.Presentation.Web.Utils;

namespace TalentHub.Presentation.Web.Endpoints.Users.UpdateProfilePicture;

public sealed class UpdateProfilePictureEndpoint :
    Ep.Req<UpdateProfilePictureRequest>
      .Res<UserDto>
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
        Guid userId = Route<Guid>("userId");

        using MemoryStream memoryStream = new();
        await req.File.CopyToAsync(memoryStream, ct);

        await SendResultAsync(
            ResultUtils.Map(
                await Resolve<ISender>().Send(
                    new UpdateProfilePictureCommand(
                        userId, 
                        memoryStream, 
                        req.File.ContentType
                    ),
                    ct
                )
            )
        );
    }
}
