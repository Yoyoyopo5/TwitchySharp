using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models;

namespace TwitchySharp.EventSub.Notifications.Channel.Goals;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.GoalEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalend">Goal End</see> for more information.
/// </remarks>
public record GoalEndNotification : EventSubNotification<GoalEndEvent, GoalEndCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.GoalEnd"/>.
/// </summary>
public record GoalEndCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.GoalEnd"/> event.
/// </summary>
public record GoalEndEvent : GoalEvent
{
    /// <summary>
    /// Indicates whether the goal was achieved (i.e., the target amount was reached when the goal ended).
    /// </summary>
    public required bool IsAchieved { get; init; }
    /// <summary>
    /// The date and time when the goal was ended by the broadcaster.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
