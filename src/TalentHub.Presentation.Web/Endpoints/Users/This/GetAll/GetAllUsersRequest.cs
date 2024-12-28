using Microsoft.AspNetCore.Mvc;
using TalentHub.Presentation.Web.Shared.RequestModels;

namespace TalentHub.Presentation.Web.Endpoints.Users.This.GetAll;

public record GetAllUsersRequest(
    [property: FromQuery(Name = "username_like")]
    string? UsernameLike,
    [property: FromQuery(Name = "email_like")]
    string? EmailLike,
    [property: FromQuery(Name = "role ")] string? Role
) : PageRequest;
