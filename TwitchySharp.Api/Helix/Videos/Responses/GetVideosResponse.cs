using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using TwitchySharp.Api.Models;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Contains a list of Twitch videos.
/// </summary>
public record GetVideosResponse
{
    /// <summary>
    /// The list of published videos that match the request filter criteria.
    /// </summary>
    public required TwitchVideo[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific video.
/// </summary>
public record TwitchVideo
{
    /// <summary>
    /// The id of the video.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The id of the stream that the video originated from.
    /// If the <see cref="Type"/> is not <see cref="TwitchVideoType.Archive"/>, this is set to <see langword="null"/>.
    /// </summary>
    public string? StreamId { get; init; }
    /// <summary>
    /// The user id of the broadcaster who owns the video.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster who owns the video.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster who owns the video.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The title of the video.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The description of the video.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// The date and time when the video was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The date and time when the video was published.
    /// </summary>
    public required DateTimeOffset PublishedAt { get; init; }
    /// <summary>
    /// The url of the video.
    /// </summary>
    public required string Url { get; init; }
    /// <summary>
    /// A templated url used to get a thumbnail image of the video.
    /// </summary>
    public required VideoThumbnailUrl ThumbnailUrl { get; init; }
    /// <summary>
    /// The video's viewable state.
    /// This is always set to <c>"public"</c>.
    /// </summary>
    public required string Viewable { get; init; }
    /// <summary>
    /// The number of times that users have watched the video.
    /// </summary>
    public required int ViewCount { get; init; }
    /// <summary>
    /// The ISO 639-1 two-letter language code that the video was broadcast in.
    /// For a list of supported languages, see <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">Supported Stream Language</see>. 
    /// The language value is <c>"other"</c> if the video was broadcast in a language not in the list of supported languages.
    /// </summary>
    public required string Language { get; init; }
    /// <summary>
    /// The video's type.
    /// </summary>
    [JsonConverter(typeof(SnakeCaseLowerJsonStringEnumConverter<TwitchVideoType>))]
    public required TwitchVideoType Type { get; init; }
    /// <summary>
    /// The length of the video.
    /// </summary>
    [JsonConverter(typeof(Iso8601TimeSpanJsonConverter))]
    public required TimeSpan Duration { get; init; }
    /// <summary>
    /// The segments that Twitch Audio Recognition muted.
    /// If there are no segments, this is set to <see langword="null"/>.
    /// </summary>
    public MutedSegment[]? MutedSegments { get; init; }
}

/// <summary>
/// Contains information about a specific muted segment in a Twitch video.
/// </summary>
public record MutedSegment
{
    /// <summary>
    /// The duration of the segment.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Duration { get; init; }
    /// <summary>
    /// The offset from the beginning of the video to where the muted segment begins.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Offset { get; init; }
}

/// <summary>
/// Possible types for Twitch videos.
/// </summary>
public enum TwitchVideoType
{
    /// <summary>
    /// An on-demand video (VOD) of one of the broadcaster's past streams.
    /// </summary>
    Archive,
    /// <summary>
    /// A highlight reel of one of the broadcaster's past streams.
    /// </summary>
    Highlight,
    /// <summary>
    /// A video that the broadcaster uploaded to their video library.
    /// </summary>
    Upload
}

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
        => writer.WriteStringValue(value.TemplateUrl);
}

internal class Iso8601TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.GetString() switch
        {
            string value => XmlConvert.ToTimeSpan("PT" + value.ToUpper()), // API docs says it returns ISO 8601 duration format, but it only returns the end, so we append PT for proper format.
            _ => default
        };

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => XmlConvert.ToString(value);
}
