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
}
