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
public record HypeTrainBeginV2Event : IHaveActiveHypeTrain, IHaveBroadcaster
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
    /// <summary>
    /// The highest level this type of Hype Train has ever reached for the broadcaster.
    /// </summary>
    public required int AllTimeHighLevel { get; init; }
    /// <summary>
    /// The highest total points this type of Hype Train has ever reached for the broadcaster.
    /// </summary>
    public required int AllTimeHighTotal { get; init; }
}
