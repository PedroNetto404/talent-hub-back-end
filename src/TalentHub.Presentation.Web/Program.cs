using TalentHub.ApplicationCore;
using TalentHub.Infra;
using TalentHub.Infra.Extensions;
using TalentHub.Presentation.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.LoadEnvs();
builder.Services
       .AddPresentation()
       .AddApplicationCore()
       .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();
app.UsePipeline();

await app.SeedDatabaseAsync();

app.DumpEndpoints();

await app.RunAsync();
