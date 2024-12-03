namespace TalentHub.Presentation.Web;

public static class Pipeline
{
    public static void UsePipeline(this WebApplication app)
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
    }

    public static void ListEndpoints(this WebApplication app)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Endpoints:");
        Console.ResetColor();

        app.Services
            .GetRequiredService<EndpointDataSource>()
            .Endpoints
            .Select(e => e.DisplayName)
            .ToList()
            .ForEach(Console.WriteLine);
    }
}
