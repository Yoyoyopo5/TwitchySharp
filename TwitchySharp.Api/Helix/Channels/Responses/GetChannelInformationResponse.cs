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
    [JsonInclude, JsonRequired]
    public ChannelInformation[] Data { get; private set; } = [];
}

public record ChannelInformation
{
    /// <summary>
    /// An ID that uniquely identifies the broadcaster (The user id of the broadcaster).
    /// </summary>
    [JsonInclude, JsonRequired]
    public string BroadcasterId { get; private set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s login name (username).
    /// </summary>
    [JsonInclude, JsonRequired]
    public string BroadcasterLogin { get; private set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string BroadcasterName { get; private set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s preferred language. The value is an ISO 639-1 two-letter language code (for example, en for English). 
    /// The value is set to “other” if the language is not a Twitch supported language.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string BroadcasterLanguage { get; private set; } = string.Empty; // Conflicted on whether I should make this an enum or leave as string
    /// <summary>
    /// The name of the game that the broadcaster is playing or last played. 
    /// The value is an empty string if the broadcaster has never played a game.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string GameName { get; private set; } = string.Empty;
    /// <summary>
    /// An ID that uniquely identifies the game that the broadcaster is playing or last played. 
    /// The value is an empty string if the broadcaster has never played a game.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string GameId { get; private set; } = string.Empty;
    /// <summary>
    /// The title of the stream that the broadcaster is currently streaming or last streamed. 
    /// The value is an empty string if the broadcaster has never streamed.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Title { get; private set; } = string.Empty;
    /// <summary>
    /// The value of the broadcaster’s stream delay setting, in seconds. 
    /// This field’s value defaults to zero unless 
    /// 1) the request specifies a user access token, 
    /// 2) the ID in the broadcaster_id query parameter matches the user ID in the access token, and 
    /// 3) the broadcaster has partner status and they set a non-zero stream delay value.
    /// </summary>
    [JsonInclude, JsonRequired]
    public uint Delay { get; private set; }
    /// <summary>
    /// The tags applied to the channel.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string[] Tags { get; private set; } = [];
    /// <summary>
    /// The CCLs applied to the channel.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string[] ContentClassificationLabels { get; private set; } = [];
    /// <summary>
    /// Boolean flag indicating if the channel has branded content.
    /// </summary>
    [JsonInclude, JsonRequired]
    public bool IsBrandedContent { get; private set; }
}
