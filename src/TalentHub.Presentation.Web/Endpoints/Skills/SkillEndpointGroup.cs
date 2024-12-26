
using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Skills;

public sealed class SKillEndpointGroup : Group
{
    public SKillEndpointGroup() => 
        Configure("skills", static ep => { });
}
