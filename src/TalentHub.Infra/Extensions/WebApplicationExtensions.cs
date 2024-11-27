using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.ApplicationCore.Courses;
using TalentHub.ApplicationCore.Universities;
using TalentHub.Infra.Data;

namespace TalentHub.Infra.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TalentHubContext>();

        await SeedEntities<University>(
            context,
            "universities_seed.json",
            p => University.Create(p).Value,
            u => u.Name,
            "universities"
        );

        await SeedEntities<Course>(
            context,
            "courses_seed.json",
            p => Course.Create(p).Value,
            c => c.Name,
            "courses"
        );
    }

    private static async Task SeedEntities<TEntity>(
        TalentHubContext context,
        string seedFileName,
        Func<string, TEntity> createEntityFunc,
        Func<TEntity, string> getEntityKey,
        string entityName
    ) where TEntity : class
    {
        var seedFilePath = GetSeedFilePath(seedFileName);

        Console.WriteLine($"{entityName} Seed Path: {seedFilePath}");
        if (!File.Exists(seedFilePath))
        {
            Console.WriteLine($"{entityName} seed file not found. Skipping seeding process.");
            return;
        }

        var fileContent = await File.ReadAllTextAsync(seedFilePath);
        if (string.IsNullOrWhiteSpace(fileContent))
        {
            Console.WriteLine($"{entityName} seed is empty. Skipping seeding process.");
            return;
        }

        var entitiesToSeed = JsonSerializer
            .Deserialize<JsonElement[]>(fileContent)!
            .Select(p => createEntityFunc(p.GetProperty("label").GetString()!))
            .ToArray();

        if (!entitiesToSeed.Any())
        {
            Console.WriteLine($"No {entityName} to seed. Skipping seeding process.");
            return;
        }

        var entityKeysInDatabase =
            await context
                .Set<TEntity>()
                .ToListAsync()
                .ContinueWith(task => task.Result.Select(getEntityKey));

        var newEntities = entitiesToSeed
            .Where(e => !entityKeysInDatabase.Contains(getEntityKey(e)))
            .ToArray();

        if (newEntities.Any())
        {
            await context.Set<TEntity>().AddRangeAsync(newEntities);
            await context.SaveChangesAsync();
            Console.WriteLine($"{newEntities.Length} {entityName} seeded successfully.");
        }
        else
        {
            Console.WriteLine($"All {entityName} are already in the database. No new {entityName} were added.");
        }
    }

    private static string GetSeedFilePath(string fileName)
    {
        var basePath = Directory.GetCurrentDirectory();

#if DEBUG
        basePath = Path.Combine(
            basePath,
            "bin",
            "Debug",
            $"net{Environment.Version.Major}.{Environment.Version.Minor}");
#endif

        return Path.Combine(basePath, fileName);
    }
}