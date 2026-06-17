using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// Contains information about a specific video.
/// </summary>
public record TwitchVideo
{
    /// <summary>
    /// The id of the video.
    /// </summary>
    public required VideoId Id { get; init; }
    /// <summary>
    /// The id of the stream that the video originated from.
    /// If the <see cref="Type"/> is not <see cref="TwitchVideoType.Archive"/>, this is set to <see langword="null"/>.
    /// </summary>
    public StreamId? StreamId { get; init; }
    /// <summary>
    /// The user id of the broadcaster who owns the video.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster who owns the video.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster who owns the video.
    /// </summary>
    public required UserName UserName { get; init; }
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
    public required Uri Url { get; init; }
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
    public required LanguageCode Language { get; init; }
    /// <summary>
    /// The video's type.
    /// </summary>
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

internal class Iso8601TimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        // We need to do E2E tests on this endpoint to discover the actual wire format.
        // Does not appear to be strict Iso8601 format including "PT".
        => reader.GetString() is string value
            ? XmlConvert.ToTimeSpan("PT" + value.ToUpper())
            : TimeSpan.Zero;

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteStringValue(XmlConvert.ToString(value).TrimStart("PT"));
}
