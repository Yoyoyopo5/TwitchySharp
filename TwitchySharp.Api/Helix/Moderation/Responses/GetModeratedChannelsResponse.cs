using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of moderated channels.
/// </summary>
public record GetModeratedChannelsResponse
{
    /// <summary>
    /// A list of channels that the user has moderator status in.
    /// </summary>
    public required ModeratedChannel[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a user's moderated channel.
/// </summary>
public record ModeratedChannel
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the user is a moderator for.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the user is a moderator for.
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the user is a moderator for.
    /// </summary>
    public required string BroadcasterName { get; init; }
}
