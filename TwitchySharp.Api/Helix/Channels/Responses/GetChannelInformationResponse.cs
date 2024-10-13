using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Contains a list of channel information.
/// </summary>
public record GetChannelInformationResponse
{
    /// <summary>
    /// A list that contains information about the specified channels. 
    /// The list is empty if the specified channels weren’t found.
    /// </summary>
    public required ChannelInformation[] Data { get; init; }
}

public record ChannelInformation
{
    /// <summary>
    /// An ID that uniquely identifies the broadcaster (The user id of the broadcaster).
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster’s login name (username).
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The broadcaster’s preferred language. The value is an ISO 639-1 two-letter language code (for example, en for English). 
    /// The value is set to “other” if the language is not a Twitch supported language.
    /// </summary>
    public required string BroadcasterLanguage { get; init; } // Conflicted on whether I should make this an enum or leave as string
    /// <summary>
    /// The name of the game that the broadcaster is playing or last played. 
    /// The value is an empty string if the broadcaster has never played a game.
    /// </summary>
    public required string GameName { get; init; }
    /// <summary>
    /// An ID that uniquely identifies the game that the broadcaster is playing or last played. 
    /// The value is an empty string if the broadcaster has never played a game.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The title of the stream that the broadcaster is currently streaming or last streamed. 
    /// The value is an empty string if the broadcaster has never streamed.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The value of the broadcaster’s stream delay setting, in seconds. 
    /// This field’s value defaults to zero unless 
    /// 1) the request specifies a user access token, 
    /// 2) the ID in the broadcaster_id query parameter matches the user ID in the access token, and 
    /// 3) the broadcaster has partner status and they set a non-zero stream delay value.
    /// </summary>
    public required uint Delay { get; init; }
    /// <summary>
    /// The tags applied to the channel.
    /// </summary>
    public required string[] Tags { get; init; }
    /// <summary>
    /// The CCLs applied to the channel.
    /// </summary>
    public required string[] ContentClassificationLabels { get; init; }
    /// <summary>
    /// Boolean flag indicating if the channel has branded content.
    /// </summary>
    public required bool IsBrandedContent { get; init; }
}
