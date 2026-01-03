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
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPollProgress"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpollprogress">Channel Poll Progress</see> for more information.
/// </remarks>
public record ChannelPollProgressNotification : EventSubNotification<ChannelPollProgressEvent, ChannelPollProgressCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPollProgress"/>.
/// </summary>
public record ChannelPollProgressCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPollBegin"/> event.
/// </summary>
public record ChannelPollProgressEvent : ChannelPollEvent
{
    /// <summary>
    /// The date and time when the poll will end.
    /// </summary>
    public required DateTimeOffset EndsAt { get; init; }
}
