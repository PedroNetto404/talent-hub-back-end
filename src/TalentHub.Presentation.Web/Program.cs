using TalentHub.ApplicationCore;
using TalentHub.Infra;
using TalentHub.Presentation.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddPresentation()
       .AddApplicationCore()
       .AddInfrastructure(builder.Configuration);

var app = builder.Build();

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

app.Run();