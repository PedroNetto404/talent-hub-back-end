using TalentHub.ApplicationCore;
using TalentHub.Infra;
using TalentHub.Infra.Extensions;
using TalentHub.Presentation.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddPresentation()
       .AddApplicationCore()
       .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();
app.UsePipeline();

app.ListEndpoints();

await Task.WhenAll(
    app.SeedDatabaseAsync(), 
    app.RunAsync()
);
