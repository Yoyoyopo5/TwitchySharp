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
/// <inheritdoc cref="EventSubSubscriptionType.ChannelPollBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelpollbegin">Channel Poll Begin</see> for more information.
/// </remarks>
public record ChannelPollBeginNotification : EventSubNotification<ChannelPollBeginEvent, ChannelPollBeginCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelPollBegin"/>.
/// </summary>
public record ChannelPollBeginCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPollBegin"/> event.
/// </summary>
public record ChannelPollBeginEvent : ChannelPollEvent
{
    /// <summary>
    /// The date and time when the poll will end.
    /// </summary>
    public required DateTimeOffset EndsAt { get; init; }
}
