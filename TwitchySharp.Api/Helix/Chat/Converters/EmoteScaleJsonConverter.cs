using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace TwitchySharp.Api.Helix.Chat;

internal class EmoteScaleJsonConverter : JsonConverter<EmoteScale>
{
    public override EmoteScale Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(EmoteScale));
        if (reader.TokenType != JsonTokenType.String)
            throw new NotSupportedException("Expected string.");
        if (reader.GetString() is not string value)
            throw new NotSupportedException("Expected string but value was null.");
        return value switch // These values are defined via Twitch API docs => https://dev.twitch.tv/docs/api/reference/#get-global-emotes
        {
            "1.0" => EmoteScale.Small,
            "2.0" => EmoteScale.Medium,
            "3.0" => EmoteScale.Large,
            _ => throw new NotSupportedException("Unsupported scale.")
        };
    }

    public override void Write(Utf8JsonWriter writer, EmoteScale value, JsonSerializerOptions options)
        => writer.WriteStringValue(
            value switch
            {
                EmoteScale.Small => "1.0",
                EmoteScale.Medium => "2.0",
                EmoteScale.Large => "3.0",
                _ => ""
            });
}
