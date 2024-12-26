using Microsoft.AspNetCore.Mvc;

namespace TalentHub.Presentation.Web.Endpoints.Users.GetById;

public sealed record GetUserByIdRequest(
    [property: FromRoute(Name = "userId")] Guid UserId
);
