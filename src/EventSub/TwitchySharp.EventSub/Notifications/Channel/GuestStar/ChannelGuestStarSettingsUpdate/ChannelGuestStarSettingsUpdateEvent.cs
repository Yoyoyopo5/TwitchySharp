namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarSettingsUpdate"/> event.
/// </summary>
public record ChannelGuestStarSettingsUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) who is hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) who is hosting the Guest Star session.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) who is hosting the Guest Star session.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
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
