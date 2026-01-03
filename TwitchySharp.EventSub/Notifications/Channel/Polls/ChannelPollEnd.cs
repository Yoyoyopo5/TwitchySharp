using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Polls;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPollEnd"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpollend">Channel Poll End</see> for more information.
/// </remarks>
public record ChannelPollEndNotification : EventSubNotification<ChannelPollEndEvent, ChannelPollEndCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPollEnd"/>.
/// </summary>
public record ChannelPollEndCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPollEnd"/> event.
/// </summary>
public record ChannelPollEndEvent : ChannelPollEvent
{
    /// <summary>
    /// The date and time when the poll ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
