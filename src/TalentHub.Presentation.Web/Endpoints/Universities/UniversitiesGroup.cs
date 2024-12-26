using FastEndpoints;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Users.Enums;

namespace TalentHub.Presentation.Web.Endpoints.Universities;

public sealed class UniversitiesGroup : Group
{
    public UniversitiesGroup() =>
    Configure("universities", static ep => 
        ep.Roles(
            nameof(Role.Admin).Underscore(),
            nameof(Role.Company).Underscore()
        )
    );
}
