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
/// <inheritdoc cref="EventSubSubscriptionType.HypeTrainBeginV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelhype_trainbegin-v2">Hype Train Begin V2</see> for more information.
/// </remarks>
public record HypeTrainBeginV2Notification : EventSubNotification<HypeTrainBeginV2Event, HypeTrainBeginV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.HypeTrainBeginV2"/>.
/// </summary>
public record HypeTrainBeginV2Condition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.HypeTrainBeginV2"/> event.
/// </summary>
public record HypeTrainBeginV2Event : HypeTrainActiveEvent
{
    /// <summary>
    /// The highest level this type of Hype Train has ever reached for the broadcaster.
    /// </summary>
    public required int AllTimeHighLevel { get; init; }
    /// <summary>
    /// The highest total points this type of Hype Train has ever reached for the broadcaster.
    /// </summary>
    public required int AllTimeHighTotal { get; init; }
}
