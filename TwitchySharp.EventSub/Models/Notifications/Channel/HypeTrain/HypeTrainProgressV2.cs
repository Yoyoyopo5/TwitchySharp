using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Channel.HypeTrain;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.HypeTrain;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.HypeTrain;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.HypeTrain;
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
public record HypeTrainProgressV2Event : IHaveActiveHypeTrain, IHaveBroadcaster
{
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the Hype Train.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    public required int Total { get; init; }
    public required int Level { get; init; }
    public SharedHypeTrainParticipant[]? SharedTrainParticipants { get; init; }
    public required DateTimeOffset StartedAt { get; init; }
    public required HypeTrainType Type { get; init; }
    public required bool IsSharedTrain { get; init; }
    public required int Progress { get; init; }
    public required int Goal { get; init; }
    public DateTimeOffset ExpiresAt { get; init; }
}
