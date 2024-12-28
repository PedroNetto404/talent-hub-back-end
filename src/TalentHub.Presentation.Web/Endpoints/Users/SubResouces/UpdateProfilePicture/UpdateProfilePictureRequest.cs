using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Users.SubResouces.UpdateProfilePicture;

public sealed record UpdateProfilePictureRequest(
    [property: FromRoute(Name = "userId")] Guid UserId,
    IFormFile File
);
