using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetEnv;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Routing;
using TalentHub.Infra.Json.Converters;

namespace TalentHub.Presentation.Web;

public static class WebApplicationExtensions
{
    public static void LoadEnvs(this WebApplicationBuilder builder)
    {
        string env = builder.Environment.EnvironmentName.ToLowerInvariant();
        string envPath = Path.Combine(
            Directory.GetCurrentDirectory(),
#if DEBUG
            "bin",
            "Debug",
            $"net{Environment.Version.Major}.{Environment.Version.Minor}",
#endif
            $".env.{env}"
        );

        Env.Load(envPath);
        builder.Configuration.AddEnvironmentVariables();
    }

    public static WebApplication UsePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication().UseAuthorization();

        app.MapHealthChecks("/health");

        app.UseFastEndpoints(static opt =>
        {
            opt.Endpoints.RoutePrefix = "api";
            opt.Versioning.Prefix = "v";
            opt.Versioning.PrependToRoute = true;
            opt.Versioning.DefaultVersion = 1;
            opt.Serializer.Options.Converters.Add(
                new JsonStringEnumConverter(
                    HumanizerSnakeCaseJsonPolicy.Instance));
            opt.Serializer.Options.PropertyNamingPolicy = HumanizerSnakeCaseJsonPolicy.Instance;
            opt.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        return app;
    }

    public static void DumpEndpoints(this WebApplication app) =>
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            using IServiceScope serviceScope = app.Services.CreateScope();

            ILogger<WebApplication> logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<WebApplication>>();
            EndpointDataSource endpointDataSource = serviceScope.ServiceProvider.GetRequiredService<EndpointDataSource>();

            logger.LogInformation(string.Join("\n", endpointDataSource.Endpoints));
        });
}
