using TalentHub.Presentation.Web.Options;

namespace TalentHub.Presentation.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services
    )
    {
        services.ConfigureOptions<ControllerOptionsSetup>();
        services.AddControllers();
        services.AddRouting();

        services.ConfigureOptions<SwaggerConfig>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
}   
