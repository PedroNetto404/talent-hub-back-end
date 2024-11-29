namespace TalentHub.Presentation.Web.Abstractions;

public interface IEndpointDefinition
{
    public void Define(IEndpointRouteBuilder routeBuilder);
}