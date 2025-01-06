using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Contains a list of found active streams.
/// </summary>
public record GetStreamsResponse
{
    /// <summary>
    /// The active streams that matched the request query.
    /// </summary>
    public required TwitchStream[] Data { get; init; }
    ///<inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific Twitch livestream.
/// </summary>
public record TwitchStream
{
    /// <summary>
    /// The id of the stream. 
    /// You can use this id later to look up the video on demand (VOD).
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The id of the category of the stream.
    /// This is an empty string is a category is not selected.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The name of the category of the stream.
    /// This is an empty string is a category is not selected.
    /// </summary>
    public required string GameName { get; init; }
    /// <summary>
    /// The type of stream.
    /// This is always set to <c>"live"</c> except when an error occurs.
    /// If an error occurs it is set to an empty string.
    /// </summary>
    public required string Type { get; init; }
    /// <summary>
    /// The title of the stream.
    /// This can be an empty string.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The tags applied to the stream.
    /// </summary>
    public required string[] Tags { get; init; }
    /// <summary>
    /// The date and time when the broadcast began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The language that the stream uses. 
    /// This is an ISO 639-1 two-letter language code or other if the stream uses a language not in the list of <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">supported stream languages</see>.
    /// </summary>
    public required string Language { get; init; }
    /// <summary>
    /// A URL template to an image of a frame from the last 5 minutes of the stream.
    /// </summary>
    public required ImageUrlTemplate ThumbnailUrl { get; init; }
    /// <summary>
    /// Indicates whether the stream is meant for mature audiences.
    /// </summary>
    public required bool IsMature { get; init; }
}
