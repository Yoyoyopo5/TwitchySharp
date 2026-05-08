using TwitchySharp.EventSub.Enums.Events.Channel.GuestStar;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.GuestStar;
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
public record ChannelGuestStarSettingsUpdateEvent : IHaveBroadcaster
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
