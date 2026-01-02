using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.Bits;

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
public record ChannelBitsUseEvent
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
    public ChannelChatMessage? Message { get; init; }
    /// <summary>
    /// The power-up associated with the bits use, if any.
    /// </summary>
    public BitsPowerUp? PowerUp { get; init; }
}

/// <summary>
/// An emote referenced in a Bits power-up.
/// </summary>
public record PowerUpEmote
{
    /// <summary>
    /// The id of the emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the emote.
    /// </summary>
    public required string Name { get; init; }
}

/// <summary>
/// A Bits power-up.
/// See <see href="https://help.twitch.tv/s/article/power-ups">Power-ups</see> for more information.
/// </summary>
public record BitsPowerUp
{
    /// <summary>
    /// The type of Power-up.
    /// </summary>
    public required BitsPowerUpType Type { get; init; }
    /// <summary>
    /// The emote that was used with the power-up, if any.
    /// </summary>
    public PowerUpEmote? Emote { get; init; }
    /// <summary>
    /// The id of the message effect that was used with the power-up, if any.
    /// </summary>
    public string? MessageEffectId { get; init; }
}

/// <summary>
/// Contains static definitions for possible Bits power up types.
/// </summary>
public record BitsPowerUpType(string Value) : ValueBackedEnum<string>(Value)
{
    public static BitsPowerUpType MessageEffect { get; } = new("message_effect");
    public static BitsPowerUpType Celebration { get; } = new("celebration");
    public static BitsPowerUpType GigantifyAnEmote { get; } = new("gigantify_an_emote");
}

/// <summary>
/// Contains static definitions for possible channel bits use types.
/// </summary>
/// <param name="Value"></param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelBitsUseType, string>))]
public record ChannelBitsUseType(string Value) : ValueBackedEnum<string>(Value)
{
    public static ChannelBitsUseType Cheer { get; } = new("cheer");
    public static ChannelBitsUseType PowerUp { get; } = new("power_up");
}
