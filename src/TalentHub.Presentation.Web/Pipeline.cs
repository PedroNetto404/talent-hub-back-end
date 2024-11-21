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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseRouting();

        app.MapControllers();
    }
}
