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
/// <inheritdoc cref="EventSubSubscriptionType.HypeTrainEndV2"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelhype_trainend-v2">Hype Train End V2</see> for more information.
/// </remarks>
public record HypeTrainEndV2Notification : EventSubNotification<HypeTrainEndV2Event, HypeTrainEndV2Condition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.HypeTrainEndV2"/>.
/// </summary>
public record HypeTrainEndV2Condition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.HypeTrainEndV2"/> event.
/// </summary>
public record HypeTrainEndV2Event : HypeTrainEvent
{
    /// <summary>
    /// The date and time when a new Hype Train can occur in the broadcaster's chat.
    /// </summary>
    public required DateTimeOffset CooldownEndsAt { get; init; }
    /// <summary>
    /// The date and time when the Hype Train ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
