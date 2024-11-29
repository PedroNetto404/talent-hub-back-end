using System.Text.Json;
using System.Text.Json.Serialization;
using Humanizer;

namespace TalentHub.Infra.Json.Converters;

public class SnakeCaseEnumConverter<TEnum> :
    JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string enumValue = reader.GetString().Pascalize();

        if (Enum.TryParse(enumValue, true, out TEnum result))
        { return result; }

        throw new JsonException($"Unable to convert \"{enumValue}\" to Enum \"{typeof(TEnum)}\".");

    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        string stringValue = Enum.GetName(typeof(TEnum), value)!;
        string snakeCaseValue = stringValue.Underscore();
        writer.WriteStringValue(snakeCaseValue);
    }
}

public class SnakeCaseEnumConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsEnum;

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
        (JsonConverter)Activator.CreateInstance(
            typeof(SnakeCaseEnumConverter<>).MakeGenericType(typeToConvert)
        )!;
}
