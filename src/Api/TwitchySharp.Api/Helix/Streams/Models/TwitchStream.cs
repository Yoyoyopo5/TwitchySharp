using System;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains information about a specific Twitch livestream.
/// </summary>
public record TwitchStream
{
    /// <summary>
    /// The id of the stream. 
    /// You can use this id later to look up the video on demand (VOD).
    /// </summary>
    public required StreamId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The id of the category of the stream.
    /// This is an empty string is a category is not selected.
    /// </summary>
    public required GameId GameId { get; init; }
    /// <summary>
    /// The name of the category of the stream.
    /// This is an empty string is a category is not selected.
    /// </summary>
    public required string GameName { get; init; }
    /// <summary>
    /// The type of stream.
    /// This is always set to <see cref="TwitchStreamType.Live"/> except when an error occurs.
    /// If an error occurs it is set to an empty string.
    /// </summary>
    public required TwitchStreamType Type { get; init; }
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
    /// This is an ISO 639-1 two-letter language code or <see cref="LanguageCode.Other"/> if the stream uses a language not in the list of <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">supported stream languages</see>.
    /// </summary>
    public required LanguageCode Language { get; init; }
    /// <summary>
    /// A URL template to an image of a frame from the last 5 minutes of the stream.
    /// </summary>
    public required ImageUrlTemplate ThumbnailUrl { get; init; }
    /// <summary>
    /// Indicates whether the stream is meant for mature audiences.
    /// </summary>
    public required bool IsMature { get; init; }
}