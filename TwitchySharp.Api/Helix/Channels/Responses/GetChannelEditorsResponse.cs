using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Contains a list of editors on a specific channel.
/// </summary>
public record GetChannelEditorsResponse
{
    /// <summary>
    /// A list of users that are editors for the specified broadcaster. 
    /// The list is empty if the broadcaster doesn’t have editors.
    /// </summary>
    [JsonInclude, JsonRequired]
    public ChannelEditor[] Data { get; private set; } = [];
}

/// <summary>
/// Contains information about a specific editor on a channel.
/// </summary>
public record ChannelEditor
{
    /// <summary>
    /// The user ID of the channel editor.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserId { get; private set; } = string.Empty;
    /// <summary>
    /// The editor's Twitch display name.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserName { get; private set; } = string.Empty;
    /// <summary>
    /// The date and time when the user became one of the broadcaster’s editors.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateTimeOffset CreatedAt { get; private set; }
}
