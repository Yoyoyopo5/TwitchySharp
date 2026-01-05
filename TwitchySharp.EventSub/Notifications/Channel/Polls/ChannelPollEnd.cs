using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.Polls;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.Polls;
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
public record ChannelPollEndEvent : IHavePoll, IHaveBroadcaster
{
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    public required string Title { get; init; }
    public required ChannelPollChoice[] Choices { get; init; }
    public required ChannelPollChannelPointsVotingSetting ChannelPointsVoting { get; init; }
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the poll ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
