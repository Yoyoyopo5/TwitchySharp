using System;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Clips;

/// <summary>
/// Contains information about a specific Twitch clip.
/// </summary>
public record TwitchClip
{
    /// <summary>
    /// An id that uniquely identifies the clip.
    /// </summary>
    public required ClipId Id { get; init; }
    /// <summary>
    /// A URL to the clip.
    /// </summary>
    public required Uri Url { get; init; }
    /// <summary>
    /// A URL that you can use in an iframe to embed the clip (see <see href="https://dev.twitch.tv/docs/embed/video-and-clips/">Embedding Video and Clips</see>).
    /// </summary>
    public required Uri EmbedUrl { get; init; }
    /// <summary>
    /// The user id of the broadcaster that the video was clipped from.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <summary>
    /// The user id of the user who created the clip.
    /// </summary>
    public required UserId CreatorId { get; init; }
    /// <summary>
    /// The creator's display name.
    /// </summary>
    public required UserName CreatorName { get; init; }
    /// <summary>
    /// An ID that identifies the video that the clip came from. 
    /// This is an empty string if the video is not available.
    /// </summary>
    public required VideoId VideoId { get; init; }
    /// <summary>
    /// The ID of the game that was being played when the clip was created.
    /// </summary>
    public required GameId GameId { get; init; }
    /// <summary>
    /// The ISO 639-1 two-letter language code that the broadcaster broadcasts in. 
    /// For example, <c>en</c> for English. 
    /// The value is other if the broadcaster uses a language that Twitch doesn’t support.
    /// </summary>
    public required LanguageCode Language { get; init; }
    /// <summary>
    /// The title of the clip.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The number of times the clip has been viewed.
    /// </summary>
    public required int ViewCount { get; init; }
    /// <summary>
    /// The date and time of when the clip was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// A URL to a thumbnail image of the clip.
    /// </summary>
    public required Uri ThumbnailUrl { get; init; }
    /// <summary>
    /// The length of the clip. Precision is 100ms.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Duration { get; init; }
    /// <summary>
    /// The zero-based offset to where the clip starts in the video (VOD). 
    /// Is <see langword="null"/> if the video is not available or hasn’t been created yet from the live stream.
    /// <br/>
    /// Note that there’s a delay between when a clip is created during a broadcast and when the offset is set. 
    /// During the delay period, this property is <see langword="null"/>. The delay is indeterminant but is typically minutes long.
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan? VodOffset { get; init; }
    /// <summary>
    /// Indicates if the clip is featured or not.
    /// </summary>
    public bool IsFeatured { get; init; }
}
