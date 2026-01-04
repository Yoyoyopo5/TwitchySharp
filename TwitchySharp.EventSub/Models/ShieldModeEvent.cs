using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models;

/// <summary>
/// The base class for Shield Mode events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.ShieldModeBegin"/>,
/// <see cref="EventSubSubscriptionType.ShieldModeEnd"/>.
/// </remarks>
public record ShieldModeEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose Shield Mode status was changed.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator who changed the Shield Mode status.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who changed the Shield Mode status.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who changed the Shield Mode status.
    /// </summary>
    public required string ModeratorUserName { get; init; }
}
