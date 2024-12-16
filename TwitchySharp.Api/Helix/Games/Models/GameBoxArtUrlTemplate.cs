using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Games;

/// <summary>
/// Helper class to create valid urls to game (category) cover images.
/// </summary>
/// <param name="TemplateUrl"></param>
[JsonConverter(typeof(GameBoxArtUrlTemplateJsonConverter))]
public record GameBoxArtUrlTemplate(string TemplateUrl)
{
    /// <summary>
    /// Creates a valid url to a game's box art.
    /// </summary>
    /// <param name="width">The width of the image to get, in pixels.</param>
    /// <param name="height">The height of the image to get, in pixels.</param>
    /// <returns>A url to an image of the specified size.</returns>
    public string MakeUrl(uint width, uint height)
        => TemplateUrl
            .Replace("{width}", width.ToString())
            .Replace("{height}", height.ToString());
}

internal class GameBoxArtUrlTemplateJsonConverter : JsonConverter<GameBoxArtUrlTemplate>
{
    public override GameBoxArtUrlTemplate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() switch
        {
            null => null,
            string value => new GameBoxArtUrlTemplate(value)
        };

    public override void Write(Utf8JsonWriter writer, GameBoxArtUrlTemplate value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.TemplateUrl);
}
