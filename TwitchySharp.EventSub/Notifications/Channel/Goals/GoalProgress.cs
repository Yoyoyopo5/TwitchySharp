using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel.Goals;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.GoalProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelgoalprogress">Goal Progress</see> for more information.
/// </remarks>
public record GoalProgressNotification : EventSubNotification<GoalProgressEvent, GoalProgressCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.GoalProgress"/>.
/// </summary>
public record GoalProgressCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.GoalProgress"/> event.
/// </summary>
public record GoalProgressEvent : GoalEvent;
