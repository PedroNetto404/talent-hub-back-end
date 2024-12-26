using FastEndpoints;

namespace TalentHub.Presentation.Web.Endpoints.Courses;

public sealed class CourseEndpointGroup : Group
{
    public CourseEndpointGroup()
    {
        Configure("courses", static ep => { });
    }
}
