using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Users.RefreshToken;

public sealed record RefreshTokenRequest(
    [property: FromRoute(Name = "userId")] Guid UserId,
    string RefreshToken
);

