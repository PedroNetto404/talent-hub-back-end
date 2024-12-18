using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Users;

public sealed class UsersEndpointsGroup : Group
{
    public UsersEndpointsGroup()
    {
        Configure("users", static ep =>
            ep.Description(static descriptionBuilder =>
                descriptionBuilder.WithDisplayName("Users Resource")));
    }
}
