using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Search;
/// <summary>
/// Contains a list of found channels.
/// </summary>
public record SearchChannelsResponse
{
    /// <summary>
    /// The list of channels.
    /// </summary>
    public required TwitchChannel[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific Twitch channel.
/// </summary>
public record TwitchChannel
{
    /// <summary>
    /// The ISO 639-1 two-letter language code of the language used by the broadcaster. 
    /// For example, <c>"en"</c> for English. 
    /// If the broadcaster uses a language not in the list of <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">supported stream languages</see>, the value is <c>"other"</c>.
    /// </summary>
    public required string BroadcasterLanguage { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster.
    /// This is what is displayed to viewers as the name of the channel.
    /// </summary>
    public required string DisplayName { get; init; }
    /// <summary>
    /// The id of the game that the broadcaster is playing or last played.
    /// If the broadcaster hasn't streamed or has streamed without a category selected, this is an empty string.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The name of the game that the broadcaster is playing or last played.
    /// If the broadcaster hasn't streamed or has streamed without a category selected, this is an empty string.
    /// </summary>
    public required string GameName { get; init; }
    /// <summary>
    /// The user id of the broadcaster.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// Indicates whether the broadcaster is streaming live. 
    /// Is <see langword="true"/> if the broadcaster is streaming live; otherwise, <see langword="false"/>.
    /// </summary>
    public required bool IsLive { get; init; }
    /// <summary>
    /// The tags applied to the channel.
    /// </summary>
    public required string[] Tags { get; init; }
    /// <summary>
    /// A URL to a thumbnail of the broadcaster’s profile image.
    /// </summary>
    public required string ThumbnailUrl { get; init; }
    /// <summary>
    /// The stream’s title. 
    /// This is an empty string if the broadcaster hasn't set a title.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The date and time when the broadcaster started streaming. 
    /// This is <see langword="null"/> if the broadcaster is not streaming live.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public required DateTimeOffset? StartedAt { get; init; }
}
