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
/// <inheritdoc cref="EventSubSubscriptionType.ShieldModeBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshield_modebegin">Shield Mode Begin</see> for more information.
/// </remarks>
public record ShieldModeBeginNotification : EventSubNotification<ShieldModeBeginEvent, ShieldModeBeginCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ShieldModeBegin"/>.
/// </summary>
public record ShieldModeBeginCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ShieldModeBegin"/> event.
/// </summary>
public record ShieldModeBeginEvent : ShieldModeEvent
{
    /// <summary>
    /// The date and time when Shield Mode was enabled.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
