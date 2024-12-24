using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using TalentHub.Infra.Json.Converters;

namespace TalentHub.Presentation.Web;

public static class WebApplicationExtensions
{
    public static WebApplication UsePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerGen();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication().UseAuthorization();
        app.MapHealthChecks("/health");
        app.UseFastEndpoints(static opt =>
        {
            JsonNamingPolicy policy = HumanizerSnakeCaseJsonPolicy.Instance;
            opt.Endpoints.RoutePrefix = "api";
            opt.Versioning.Prefix = "v";
            opt.Versioning.PrependToRoute = true;
            opt.Versioning.DefaultVersion = 1;
            opt.Serializer.Options.Converters.Add(new JsonStringEnumConverter(policy));
            opt.Serializer.Options.PropertyNamingPolicy = policy;
        });

        return app;
    }
}
