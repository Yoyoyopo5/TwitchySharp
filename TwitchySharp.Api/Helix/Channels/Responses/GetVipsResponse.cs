using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Contains a list of VIPs for a specific channel.
/// </summary>
public record GetVipsResponse
{
    /// <summary>
    /// The list of VIPs.
    /// </summary>
    public required ChannelVip[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific VIP on a channel.
/// </summary>
public record ChannelVip
{
    /// <summary>
    /// The user id of the VIP.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the VIP.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the VIP.
    /// </summary>
    public required string UserLogin { get; init; }
}
