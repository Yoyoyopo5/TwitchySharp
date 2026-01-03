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
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPredictionLock"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpredictionbegin">Channel Prediction Begin</see> for more information.
/// </remarks>
public record ChannelPredictionBeginNotification : EventSubNotification<ChannelPredictionBeginEvent, ChannelPredictionBeginCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPredictionBegin"/>.
/// </summary>
public record ChannelPredictionBeginCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPredictionBegin"/> event.
/// </summary>
public record ChannelPredictionBeginEvent : ChannelPredictionEvent
{
    /// <summary>
    /// The date and time when the prediction will lock (no more bets can be made).
    /// </summary>
    public required DateTimeOffset LocksAt { get; init; }
}
