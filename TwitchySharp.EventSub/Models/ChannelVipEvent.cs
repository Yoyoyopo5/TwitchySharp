using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for Channel VIP events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ChannelVIPAdd"/>,
/// <see cref="EventSubSubscriptionType.ChannelVIPRemove"/>.
/// </remarks>
public record ChannelVipEvent
{
    /// <summary>
    /// The id of the user added or removed as a VIP.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user added or removed as a VIP.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user added or removed as a VIP.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the VIP was added or removed.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the VIP was added or removed.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the VIP was added or removed.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
}
