using System.Reflection;

namespace TalentHub.Presentation.Web.Abstractions;

public static class DefineEndpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v{version:apiVersion}");

        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(p => 
                p.IsAssignableTo(typeof(IEndpointDefinition)) 
                && !p.IsInterface 
                && !p.IsAbstract
            )
            .ToList()
            .ForEach(p => 
            {
                var instance = (IEndpointDefinition)Activator.CreateInstance(p)!;
                instance.Define(group);
            });
    }
}