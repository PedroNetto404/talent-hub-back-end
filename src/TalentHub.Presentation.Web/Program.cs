using TalentHub.ApplicationCore;
using TalentHub.Infra;
using TalentHub.Presentation.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddPresentation()
       .AddApplicationCore()
       .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UsePipeline();
app.Run(); 