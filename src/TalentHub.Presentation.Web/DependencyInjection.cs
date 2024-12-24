using System.Text.Json.Serialization;
using FastEndpoints;
using TalentHub.Infra.Json.Converters;

namespace TalentHub.Presentation.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection s)
    {
        s.AddFastEndpoints(static opt => opt.IncludeAbstractValidators = true);
        s.AddSwaggerDocument(c => c.Title = "Talent Hub Api");
        s.AddEndpointsApiExplorer();
        s.AddHealthChecks();
        s.AddRouting();
        s.ConfigureHttpJsonOptions(opt =>
        {
            opt.SerializerOptions.PropertyNamingPolicy = HumanizerSnakeCaseJsonPolicy.Instance;
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(HumanizerSnakeCaseJsonPolicy.Instance));
        });

        return s;
    }
}
