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
using TwitchySharp.Shared.EventSub.Enums;

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
public record HypeTrainEndV2Event : IHaveHypeTrain, IHaveBroadcaster
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
    /// <summary>
    /// The date and time when a new Hype Train can occur in the broadcaster's chat.
    /// </summary>
    public required DateTimeOffset CooldownEndsAt { get; init; }
    /// <summary>
    /// The date and time when the Hype Train ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
