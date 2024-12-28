using System.Text;
using System.Text.Json.Serialization;
using DotNetEnv;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Spectre.Console;
using TalentHub.Infra.Json.Converters;
using static System.String;

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

    public static void UseLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .ReadFrom
            .Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();
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
            EndpointDataSource endpointDataSource =
                serviceScope.ServiceProvider.GetRequiredService<EndpointDataSource>();

            var endpoints = endpointDataSource.Endpoints
                .OfType<RouteEndpoint>()
                .Select(endpoint => new
                {
                    Method = 
                        endpoint
                            .Metadata
                            .OfType<HttpMethodMetadata>()
                            .FirstOrDefault()?
                            .HttpMethods?
                            .FirstOrDefault() ?? "UNKNOWN",
                    Uri = endpoint.RoutePattern.RawText ?? "UNKNOWN",
                    AllowAnonymous = 
                        endpoint
                            .Metadata
                            .OfType<AllowAnonymousAttribute>()
                            .Any() ? "Yes" : "No"
                })
                .OrderBy(e => e.Uri)
                .ThenBy(e => e.Method) 
                .ToList();

            var table = new Table();
            table.AddColumn("METHOD");
            table.AddColumn("URI");
            table.AddColumn("ALLOW ANONYMOUS");

            foreach (var endpoint in endpoints)
            {
                table.AddRow(endpoint.Method, endpoint.Uri, endpoint.AllowAnonymous);
            }

            AnsiConsole.Write(
                new Panel(table)
                    .Header("Endpoints", Justify.Center)
                    .Border(BoxBorder.Rounded)
                    .Expand()
            );
        });
}
