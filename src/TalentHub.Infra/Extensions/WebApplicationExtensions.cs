using System.Text.Json;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.ApplicationCore.Courses;
using TalentHub.ApplicationCore.Extensions;
using TalentHub.Infra.Data;
using NetFile = System.IO.File;

namespace TalentHub.Infra.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        using var context = scope.ServiceProvider.GetRequiredService<TalentHubContext>();

        var courses = await context.Set<Course>().ToListAsync();
        var courseSeedPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "seed",
            "courses_seed.json"
        );

        var coursesSeedFileContent = await NetFile.ReadAllTextAsync(courseSeedPath);
        var seedCourses = coursesSeedFileContent.FromJson<JsonElement[]>();
    }
}