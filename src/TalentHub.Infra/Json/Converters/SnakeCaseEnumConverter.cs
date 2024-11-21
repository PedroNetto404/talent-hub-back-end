using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Humanizer;
using TalentHub.ApplicationCore.Extensions;

namespace TalentHub.Infra.Json.Converters;

public class SnakeCaseEnumConverter<TEnum> :
    JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var enumValue = reader.GetString()?.Replace("_", "", StringComparison.OrdinalIgnoreCase);

        if (Enum.TryParse(enumValue, true, out TEnum result))
            return result;

        throw new JsonException($"Unable to convert \"{enumValue}\" to Enum \"{typeof(TEnum)}\".");

    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        var stringValue = Enum.GetName(typeof(TEnum), value)!;
        var snakeCaseValue = stringValue.Underscore();
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