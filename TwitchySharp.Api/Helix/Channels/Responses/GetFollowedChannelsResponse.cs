using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Contains the list of a user's followed channels.
/// </summary>
public record GetFollowedChannelsResponse
{
    /// <summary>
    /// The list of broadcasters that the user follows. 
    /// The list is in descending order by the <see cref="FollowedChannel.FollowedAt"/> property (with the most recently followed broadcaster first). 
    /// The list is empty if the user doesn’t follow anyone.
    /// </summary>
    [JsonInclude, JsonRequired]
    public FollowedChannel[] Data { get; private set; } = [];
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages left to page through.
    /// </summary>
    [JsonInclude, JsonRequired]
    public Pagination Pagination { get; private set; } = new();
    /// <summary>
    /// The total number of broadcasters that the user follows. 
    /// As someone pages through the list, the number may change as the user follows or unfollows broadcasters.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int Total { get; private set; }
}

public record FollowedChannel
{
    /// <summary>
    /// The user ID of the the broadcaster that this user is following.
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
    /// The time when the user started following the broadcaster.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateTimeOffset FollowedAt { get; private set; }
}
