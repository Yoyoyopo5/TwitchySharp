using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Models.Helix.Chat.Models;

namespace TwitchySharp.Api.Models.Helix.Chat.Converters;

internal class EmoteImageTemplateStringJsonConverter : JsonConverter<EmoteImageTemplateString>
{
    public override EmoteImageTemplateString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(EmoteImageTemplateString));
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Unexpected {reader.TokenType} when reading {typeof(EmoteImageTemplateString)}.");
        if (reader.GetString() is not string value)
            throw new JsonException($"Unexpected null string value when reading {typeof(EmoteImageTemplateString)}");
        return new EmoteImageTemplateString() { TemplateString = value };
    }

    public override void Write(Utf8JsonWriter writer, EmoteImageTemplateString value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.TemplateString);
}
