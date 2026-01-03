using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Warning;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelWarningSend"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelwarningsend">Channel Warning Send</see> for more information.
/// </remarks>
public record ChannelWarningSendNotification : EventSubNotification<ChannelWarningSendEvent, ChannelWarningSendCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelWarningSend"/>.
/// </summary>
public record ChannelWarningSendCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelWarningSend"/> event.
/// </summary>
public record ChannelWarningSendEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the warning was issued.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the warning was issued.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the warning was issued.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator who sent the warning.
    /// </summary>
    public required string ModeratorUserId { get; init; }
    /// <summary>
    /// The login (username) of the moderator who sent the warning.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator who sent the warning.
    /// </summary>
    public required string ModeratorUserName { get; init; }
    /// <summary>
    /// The id of the user that received the warning.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that received the warning.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that received the warning.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The reason given for the warning by the moderator, if any.
    /// </summary>
    public string? Reason { get; init; }
    /// <summary>
    /// The chat rules cited for the warning by the moderator, if any.
    /// </summary>
    public string[]? ChatRulesCited { get; init; }
}
