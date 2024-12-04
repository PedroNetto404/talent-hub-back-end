using System.Text.Json;
using System.Text.Json.Serialization;

namespace TalentHub.Infra.Json.Converters;

public class SnakeCaseEnumConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsEnum;

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        (JsonConverter)Activator.CreateInstance(
            typeof(SnakeCaseEnumConverter<>).MakeGenericType(typeToConvert)
        )!;
}