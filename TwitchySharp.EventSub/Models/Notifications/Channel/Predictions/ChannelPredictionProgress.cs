using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Predictions;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.Predictions;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Predictions;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPredictionProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpredictionprogress">Channel Prediction Progress</see> for more information.
/// </remarks>
public record ChannelPredictionProgressNotification : EventSubNotification<ChannelPredictionProgressEvent, ChannelPredictionProgressCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPredictionProgress"/>.
/// </summary>
public record ChannelPredictionProgressCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPredictionProgress"/> event.
/// </summary>
public record ChannelPredictionProgressEvent : IHavePrediction, IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that is hosting the prediction.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required ChannePredictionOutcome[] Outcomes { get; init; }
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the prediction will lock (no more bets can be made).
    /// </summary>
    public required DateTimeOffset LocksAt { get; init; }
}
