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
    public required FollowedChannel[] Data { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results. 
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
    /// <summary>
    /// The total number of broadcasters that the user follows. 
    /// As someone pages through the list, the number may change as the user follows or unfollows broadcasters.
    /// </summary>
    public required int Total { get; init; }
}

public record FollowedChannel
{
    /// <summary>
    /// The user ID of the the broadcaster that this user is following.
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
    /// The time when the user started following the broadcaster.
    /// </summary>
    public required DateTimeOffset FollowedAt { get; init; }
}
