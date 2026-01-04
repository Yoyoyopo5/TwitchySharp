using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Notifications.Channel.HypeTrain;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.HypeTrainProgressV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelhype_trainprogress-v2">Hype Train Progress V2</see> for more information.
/// </remarks>
public record HypeTrainProgressV2Notification : EventSubNotification<HypeTrainProgressV2Event, HypeTrainProgressV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.HypeTrainProgressV2"/>.
/// </summary>
public record HypeTrainProgressV2Condition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.HypeTrainProgressV2"/> event.
/// </summary>
public record HypeTrainProgressV2Event : HypeTrainActiveEvent;
