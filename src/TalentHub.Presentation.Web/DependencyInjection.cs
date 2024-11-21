using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TalentHub.Infra.Json.Converters;
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
            options.JsonSerializerOptions.Converters.Add(new SnakeCaseEnumConverterFactory());
        });

        services.AddRouting(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "TalentHub API",
                Description = "API Documentation for TalentHub"
            });
        });
        
        services.AddEndpointsApiExplorer();

        return services;
    }
}
