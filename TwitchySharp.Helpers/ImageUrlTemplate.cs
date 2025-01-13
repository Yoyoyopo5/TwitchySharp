using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace TwitchySharp.Helpers;

/// <summary>
/// Helper class to create valid urls to game (category) cover images and stream thumbnails.
/// </summary>
/// <param name="TemplateUrl"></param>
[JsonConverter(typeof(ImageUrlTemplateJsonConverter))]
public record ImageUrlTemplate(string TemplateUrl)
{
    /// <summary>
    /// Creates a valid url to an image based on the requested width and height.
    /// </summary>
    /// <param name="width">The width of the image to get, in pixels.</param>
    /// <param name="height">The height of the image to get, in pixels.</param>
    /// <returns>A url to an image of the specified size.</returns>
    public string ToImageUrl(uint width, uint height)
        => TemplateUrl
            .Replace("{width}", width.ToString())
            .Replace("{height}", height.ToString());
}

internal class ImageUrlTemplateJsonConverter : JsonConverter<ImageUrlTemplate>
{
    public override ImageUrlTemplate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() switch
        {
            null => null,
            string value => new ImageUrlTemplate(value)
        };

    public override void Write(Utf8JsonWriter writer, ImageUrlTemplate value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.TemplateUrl);
}
