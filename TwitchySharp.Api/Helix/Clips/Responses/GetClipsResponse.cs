using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Contains a list of Twitch clips.
/// </summary>
public record GetClipsResponse
{
    /// <summary>
    /// The list of video clips.
    /// For clips returned by GameId or BroadcasterId, the list is in descending order by view count. 
    /// For lists returned by id, the list is in the same order as the input ids.
    /// </summary>
    public required TwitchClip[] Data { get; init; }
    /// <summary>
    /// The information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific Twitch clip.
/// </summary>
public record TwitchClip
{
    /// <summary>
    /// An id that uniquely identifies the clip.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// A URL to the clip.
    /// </summary>
    public required string Url { get; init; }
    /// <summary>
    /// A URL that you can use in an iframe to embed the clip (see <see href="https://dev.twitch.tv/docs/embed/video-and-clips/">Embedding Video and Clips</see>).
    /// </summary>
    public required string EmbedUrl { get; init; }
    /// <summary>
    /// The user id of the broadcaster that the video was clipped from.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The user id of the user who created the clip.
    /// </summary>
    public required string CreatorId { get; init; }
    /// <summary>
    /// The creator's display name.
    /// </summary>
    public required string CreatorName { get; init; }
    /// <summary>
    /// An ID that identifies the video that the clip came from. 
    /// This is an empty string if the video is not available.
    /// </summary>
    public required string VideoId { get; init; }
    /// <summary>
    /// The ID of the game that was being played when the clip was created.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The ISO 639-1 two-letter language code that the broadcaster broadcasts in. 
    /// For example, en for English. 
    /// The value is other if the broadcaster uses a language that Twitch doesn’t support.
    /// </summary>
    public required string Language { get; init; }
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
    public required string ThumbnailUrl { get; init; }
    /// <summary>
    /// The length of the clip, in <b>seconds</b>. Precision is 0.1.
    /// </summary>
    public required double Duration { get; init; }
    /// <summary>
    /// The zero-based offset, in <b>seconds</b>, to where the clip starts in the video (VOD). 
    /// Is <see langword="null"/> if the video is not available or hasn’t been created yet from the live stream.
    /// <br/>
    /// Note that there’s a delay between when a clip is created during a broadcast and when the offset is set. 
    /// During the delay period, this property is <see langword="null"/>. The delay is indeterminant but is typically minutes long.
    /// </summary>
    public int? VodOffset { get; init; }
    /// <summary>
    /// Indicates if the clip is featured or not.
    /// </summary>
    public bool IsFeatured { get; init; }
}
