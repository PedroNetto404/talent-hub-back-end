using FastEndpoints;
using FastEndpoints.Swagger;
using TalentHub.Presentation.Web.Options;

namespace TalentHub.Presentation.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services
    )
    {
        services.AddFastEndpoints().SwaggerDocument(static options =>
            {
                options.MaxEndpointVersion = 1;
                options.DocumentSettings = s => 
                {
                    s.DocumentName = "TalentHub V1";
                    s.Title = "TalentHub";
                    s.Version = "v1";
                };
            });

        services.AddHealthChecks();

        services.ConfigureOptions<ControllerOptionsSetup>();
        services.AddControllers();
        services.AddRouting();

        services.ConfigureOptions<SwaggerConfig>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
}   
