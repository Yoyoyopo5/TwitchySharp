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
/// <inheritdoc cref="EventSubSubscriptionType.GoalBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalbegin">Goal Begin</see> for more information.
/// </remarks>
public record GoalBeginNotification : EventSubNotification<GoalBeginEvent, GoalBeginCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.GoalBegin"/>.
/// </summary>
public record GoalBeginCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.GoalBegin"/> event.
/// </summary>
public record GoalBeginEvent : GoalEvent;
