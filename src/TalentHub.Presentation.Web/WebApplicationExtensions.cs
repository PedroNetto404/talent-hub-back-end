using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;

namespace TalentHub.Presentation.Web;

public static class WebApplicationExtensions
{
    public static WebApplication UsePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerGen();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/health");

        app.MapControllers();

        app.UseFastEndpoints(static options =>
        {
            options.Endpoints.RoutePrefix = "api";
            options.Versioning.Prefix = "v";
            options.Versioning.PrependToRoute = true;
            options.Versioning.DefaultVersion = 1;
            options.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        });

        return app;
    }
}
