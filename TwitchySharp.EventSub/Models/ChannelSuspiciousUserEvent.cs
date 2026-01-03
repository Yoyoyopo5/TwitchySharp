using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for Channel Suspicious User events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ChannelSuspiciousUserMessage"/>,
/// <see cref="EventSubSubscriptionType.ChannelSuspiciousUserUpdate"/>.
/// </remarks>
public record ChannelSuspiciousUserEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) in whose chat the suspicious user event occurred.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The user id of the suspicious user.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The display name of the suspicious user.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The login (username) of the suspicious user.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The current status of the suspicious user as set by a moderator.
    /// </summary>
    public required SuspiciousUserStatus LowTrustStatus { get; init; }
}

/// <summary>
/// Contains static definitions for possible suspicious user statuses.
/// </summary>
/// <param name="Value">The string value of the suspicious user status.</param>
public record SuspiciousUserStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static SuspiciousUserStatus None { get; } = new("none");
    public static SuspiciousUserStatus ActiveMonitoring { get; } = new("active_monitoring");
    public static SuspiciousUserStatus Restricted { get; } = new("restricted");
}
