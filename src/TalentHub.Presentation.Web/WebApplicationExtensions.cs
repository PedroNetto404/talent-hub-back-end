using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace TalentHub.Presentation.Web;

public static class WebApplicationExtensions
{
    public static WebApplication UsePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/health");

        app.MapControllers();

        return app;
    }

    public static WebApplication PrintEndpoints(this WebApplication app)
    {

        ILogger<WebApplication> logger = app.Services.GetRequiredService<ILogger<WebApplication>>();
        ISwaggerProvider swaggerProvider = app.Services.GetRequiredService<ISwaggerProvider>();
        OpenApiDocument swagger = swaggerProvider.GetSwagger("v1");

        IOrderedEnumerable<IGrouping<string, KeyValuePair<string, OpenApiPathItem>>> groupedPaths =
            swagger.Paths
                .GroupBy(p => GetResourceName(p.Key))
                .OrderBy(g => g.Key);


        StringBuilder logBuilder = new();

        foreach (IGrouping<string, KeyValuePair<string, OpenApiPathItem>> group in groupedPaths)
        {

            IOrderedEnumerable<IGrouping<string, KeyValuePair<string, OpenApiPathItem>>> subGroupedPaths =
                group
                    .GroupBy(p => GetSubResourceName(p.Key))
                    .OrderBy(g => g.Key);

            logBuilder.AppendLine($"\nRecurso: {group.Key}");
            foreach (IGrouping<string, KeyValuePair<string, OpenApiPathItem>> subGroup in subGroupedPaths)
            {
                if (!string.IsNullOrEmpty(subGroup.Key))
                {
                    logBuilder.AppendLine($"\tSubrecurso: {subGroup.Key}");
                }

                foreach (KeyValuePair<string, OpenApiPathItem> path in subGroup.OrderBy(p => p.Key))
                {
                    foreach (KeyValuePair<OperationType, OpenApiOperation> operation in path.Value.Operations)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\t\t{operation.Key.ToString().ToUpper()} {path.Key}");
                        Console.ResetColor();
                    }
                }
            }

            Console.WriteLine();
        }

        return app;
    }

    private static string GetResourceName(string path)
    {
        string[] segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        return segments.Length > 1 ? segments[1] : segments.FirstOrDefault() ?? string.Empty;
    }

    private static string GetSubResourceName(string path)
    {
        string[] segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length > 2)
        {
            if (segments[2].StartsWith('{'))
            {
                return segments.Length > 3 ? segments[3] : string.Empty;
            }
            
            return segments[2];
        }
        return string.Empty;
    }
}
