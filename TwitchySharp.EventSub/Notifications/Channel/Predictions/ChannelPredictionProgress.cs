using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Predictions;
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
public record ChannelPredictionProgressEvent : ChannelPredictionEvent
{
    /// <summary>
    /// The date and time when the prediction will lock (no more bets can be made).
    /// </summary>
    public required DateTimeOffset LocksAt { get; init; }
}
