using FastEndpoints;
using Humanizer;
using TalentHub.ApplicationCore.Resources.Users.Enums;

namespace TalentHub.Presentation.Web.Endpoints.CompanySectors;

public sealed class CompanySectorsGroup : Group
{
    public CompanySectorsGroup() =>
        Configure("company-sectors", static ep =>
            ep.Roles(
                nameof(Role.Admin).Underscore(), 
                nameof(Role.Company).Underscore()
            )
        );
}
