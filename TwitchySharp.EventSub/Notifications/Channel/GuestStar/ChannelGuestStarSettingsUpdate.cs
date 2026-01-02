using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.GuestStar;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarSettingsUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_settingsupdate">Channel Guest Star Settings Update</see> for more information.
/// </remarks>
public record ChannelGuestStarSettingsUpdateNotification : EventSubNotification<ChannelGuestStarSettingsUpdateEvent, ChannelGuestStarSettingsUpdateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelGuestStarSettingsUpdate"/>.
/// </summary>
public record ChannelGuestStarSettingsUpdateCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarSettingsUpdate"/> event.
/// </summary>
public record ChannelGuestStarSettingsUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) who is hosting the Guest Star session.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) who is hosting the Guest Star session.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) who is hosting the Guest Star session.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// Indicates whether Guest Star moderators have control over a guest's live status once they are assigned to a slot.
    /// </summary>
    public required bool IsModeratorSendLiveEnabled { get; init; }
    /// <summary>
    /// The number of slots the Guest Star session will allow the host to add.
    /// </summary>
    public required int SlotCount { get; init; }
    /// <summary>
    /// Indicates whether browser sources subscribed to sessions on this channel should output audio.
    /// </summary>
    public required bool IsBrowserSourceAudioEnabled { get; init; }
    /// <summary>
    /// The layout of guests within a Guest Star session.
    /// </summary>
    public required GuestStarGroupLayout GroupLayout { get; init; }
}

/// <summary>
/// Contains static definitions for possible Guest Star group layout types.
/// </summary>
/// <param name="Value">The string value of the layout type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<GuestStarGroupLayout, string>))]
public record GuestStarGroupLayout(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// All live guests are tiled within the browser source with the same size. 
    /// </summary>
    public static GuestStarGroupLayout Tiled { get; } = new("tiled");
    /// <summary>
    /// All live guests are tiled within the browser source with the same size. 
    /// If there is an active screen share, it is sized larger than the other guests.
    /// </summary>
    public static GuestStarGroupLayout Screenshare { get; } = new("screenshare");
    /// <summary>
    /// Indicates the group layout will contain all participants in a top-aligned horizontal stack.
    /// </summary>
    public static GuestStarGroupLayout HorizontalTop { get; } = new("horizontal_top");
    /// <summary>
    /// Indicates the group layout will contain all participants in a bottom-aligned horizontal stack.
    /// </summary>
    public static GuestStarGroupLayout HorizontalBottom { get; } = new("horizontal_bottom");
    /// <summary>
    /// Indicates the group layout will contain all participants in a left-aligned vertical stack.
    /// </summary>
    public static GuestStarGroupLayout VerticalLeft { get; } = new("vertical_left");
    /// <summary>
    /// Indicates the group layout will contain all participants in a right-aligned vertical stack.
    /// </summary>
    public static GuestStarGroupLayout VerticalRight { get; } = new("vertical_right");
}
