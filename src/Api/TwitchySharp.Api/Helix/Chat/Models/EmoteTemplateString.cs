using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Holds template URL information for an emote's CDN image link.
/// Use <see cref="CreateEmoteImageUrl(string, EmoteFormat, EmoteTheme, EmoteScale)"/>
/// to create a URL pointing to a specific emote's image data. 
/// </summary>
[JsonConverter(typeof(EmoteImageTemplateStringJsonConverter))]
public readonly record struct EmoteImageTemplateString
{
    /// <summary>
    /// The template string for the emote. 
    /// This is returned from the Twitch API in some responses (e.g. <see cref="GetChannelEmotesResponse"/>).
    /// </summary>
    public required string TemplateString { get; init; } // Considered making internal but whatever. Use with care.
    /// <summary>
    /// Creates a CDN request URL as outlined in <see href="https://dev.twitch.tv/docs/chat/send-receive-messages/#cdn-template">CDN template</see>.
    /// Use the returned URL to make a request for an emote's image data.
    /// </summary>
    /// <param name="emoteId">The id of the emote.</param>
    /// <param name="format">The format to get the image in.</param>
    /// <param name="theme">The background theme to get the image in.</param>
    /// <param name="scale">The scale to get the emote in.</param>
    /// <returns></returns>
    public string CreateEmoteImageUrl(EmoteId emoteId, EmoteFormat format, EmoteTheme theme, EmoteScale scale)
    {
        return TemplateString
            .Replace("{{id}}", emoteId.Value)
            .Replace("{{format}}", format.Value)
            .Replace("{{theme_mode}}", theme.Value)
            .Replace("{{scale}}", scale.Value);
    }
    /// <summary>
    /// </summary>
    /// <returns>The <see cref="TemplateString"/></returns>
    public override string ToString()
        => TemplateString;
}

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
