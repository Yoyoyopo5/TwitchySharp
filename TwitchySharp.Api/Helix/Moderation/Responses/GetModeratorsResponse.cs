using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of a channel's moderators.
/// </summary>
public record GetModeratorsResponse
{
    /// <summary>
    /// The list of moderators for the specified channel.
    /// </summary>
    public required Moderator[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific channel moderator.
/// </summary>
public record Moderator
{
    /// <summary>
    /// The user id of the moderator.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator.
    /// </summary>
    public required string UserName { get; init; }
}
