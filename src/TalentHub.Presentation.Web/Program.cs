using DotNetEnv;
using TalentHub.ApplicationCore;
using TalentHub.Infra;
using TalentHub.Infra.Extensions;
using TalentHub.Presentation.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

LoadEnvs();

builder.Services
       .AddPresentation()
       .AddApplicationCore() 
       .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();
app.UsePipeline();

await app.SeedDatabaseAsync();

await app.RunAsync();

void LoadEnvs()
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
