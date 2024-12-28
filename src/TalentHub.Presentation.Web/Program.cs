using Serilog;
using TalentHub.ApplicationCore;
using TalentHub.Infra;
using TalentHub.Infra.Extensions;
using TalentHub.Presentation.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.LoadEnvs();
builder.UseLogger();

builder.Services
       .AddPresentation()
       .AddApplicationCore()
       .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();
app.UsePipeline();

await app.SeedDatabaseAsync();

app.DumpEndpoints();
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

await app.RunAsync();
