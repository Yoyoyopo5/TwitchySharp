using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Enums.Events.Channel.Bits;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.Bits;
using TwitchySharp.EventSub.Models.Notifications;
using TwitchySharp.EventSub.Notifications.Channel.Chat;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Bits;

/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelBitsUse"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelbitsuse">Channel Bits Use</see> for more information.
/// </remarks>
public record ChannelBitsUseNotification : EventSubNotification<ChannelBitsUseEvent, ChannelBitsUseCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelBitsUse"/>.
/// </summary>
public record ChannelBitsUseCondition : BroadcasterCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelBitsUse"/> event.
/// </summary>
public record ChannelBitsUseEvent : IHaveBroadcaster, IHaveUser
{
    /// <summary>
    /// The user id of the broadcaster (channel) where the bits were used.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) where the bits were used.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) where the bits were used.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that used the bits.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that used the bits.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that used the bits.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The number of bits that were used.
    /// </summary>
    public required int Bits { get; init; }
    /// <summary>
    /// The type of bits use.
    /// </summary>
    public required ChannelBitsUseType Type { get; init; }
    /// <summary>
    /// The message associated with the bits use, if any.
    /// </summary>
    public BitsChatMessage? Message { get; init; }
    /// <summary>
    /// The power-up associated with the bits use, if any.
    /// </summary>
    public BitsPowerUp? PowerUp { get; init; }
}
