using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for Stream events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.StreamOnline"/>,
/// <see cref="EventSubSubscriptionType.StreamOffline"/>.
/// </remarks>
public record StreamEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose stream status changed.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose stream status changed.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose stream status changed.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
}
