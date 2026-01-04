using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel.ShieldMode;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ShieldModeEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshield_modeend">Shield Mode End</see> for more information.
/// </remarks>
public record ShieldModeEndNotification : EventSubNotification<ShieldModeEndEvent, ShieldModeEndCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ShieldModeEnd"/>.
/// </summary>
public record ShieldModeEndCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShieldModeEnd"/> event.
/// </summary>
public record ShieldModeEndEvent : ShieldModeEvent
{
    /// <summary>
    /// The date and time when Shield Mode was disabled.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
