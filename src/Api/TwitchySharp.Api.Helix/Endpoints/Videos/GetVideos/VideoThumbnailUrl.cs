using System.Text.Json;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// A template used to get image URLs for Twitch videos via <see cref="ToImageUrl(uint, uint)"/>.
/// Reccomended sizes are 320x180 and multiples.
/// </summary>
[JsonConverter(typeof(VideoThumbnailUrlJsonConverter))]
public record VideoThumbnailUrl : ImageUrlTemplate
{
    public VideoThumbnailUrl(string TemplateUrl) : base(TemplateUrl)
        => (WidthTemplate, HeightTemplate) = ("%{width}", "%{height}");
}

internal class VideoThumbnailUrlJsonConverter : JsonConverter<VideoThumbnailUrl>
{
    public override VideoThumbnailUrl? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() switch
        {
            string value => new VideoThumbnailUrl(value),
            _ => null
        };

    public override void Write(Utf8JsonWriter writer, VideoThumbnailUrl value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value);
}
