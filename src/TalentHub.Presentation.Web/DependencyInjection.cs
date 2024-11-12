using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TalentHub.Presentation.Web.Binders;

namespace TalentHub.Presentation.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        var controllerBuilder = services.AddControllers(options =>
        {
            options.ModelBinderProviders.Insert(0, new DateOnlyModelBinderProvider());
        });

        controllerBuilder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddRouting(options => 
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

        services.AddSwaggerGen();

        services.AddEndpointsApiExplorer();

        return services;
    }
}
