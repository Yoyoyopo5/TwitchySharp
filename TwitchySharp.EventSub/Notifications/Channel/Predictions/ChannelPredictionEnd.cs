using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Predictions;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPredictionEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpredictionend">Channel Prediction End</see> for more information.
/// </remarks>
public record ChannelPredictionEndNotification : EventSubNotification<ChannelPredictionEndEvent, ChannelPredictionEndCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPredictionEnd"/>.
/// </summary>
public record ChannelPredictionEndCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPredictionEnd"/> event.
/// </summary>
public record ChannelPredictionEndEvent : ChannelPredictionEvent
{
    /// <summary>
    /// The status of the ended prediction.
    /// </summary>
    public required ChannelPredictionStatus Status { get; init; }
    /// <summary>
    /// The date and time when the prediction ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
/// <summary>
/// Contains static definitions for possible ended channel prediction statuses.
/// </summary>
/// <param name="Value">The string value of the prediction status.</param>
public record ChannelPredictionStatus(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelPredictionStatus Resolved { get; } = new("resolved");
    public static ChannelPredictionStatus Canceled { get; } = new("canceled");
}
