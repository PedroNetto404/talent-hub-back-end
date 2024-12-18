namespace TalentHub.Presentation.Web.Endpoints.Users.UpdateProfilePicture;

public sealed record UpdateProfilePictureRequest
{
    public IFormFile File { get; set; }
}
