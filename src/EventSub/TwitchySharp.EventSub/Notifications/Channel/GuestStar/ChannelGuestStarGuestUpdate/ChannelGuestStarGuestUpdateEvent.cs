namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarGuestUpdate"/> event.
/// </summary>
public record ChannelGuestStarGuestUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) present in the Guest Star session who this subscription is associated with.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) present in the Guest Star session who this subscription is associated with.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) present in the Guest Star session who this subscription is associated with.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the Guest Star session.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }
    /// <summary>
    /// The user id of the moderator who updated the guest's state.
    /// This is <see langword="null"/> if the guest updated their own state.
    /// </summary>
    public UserId? ModeratorUserId { get; init; }
    /// <summary>
    /// The display name of the moderator who updated the guest's state.
    /// This is <see langword="null"/> if the guest updated their own state.
    /// </summary>
    public UserName? ModeratorUserName { get; init; }
    /// <summary>
    /// The login (username) of the moderator who updated the guest's state.
    /// This is <see langword="null"/> if the guest updated their own state.
    /// </summary>
    public UserLogin? ModeratorUserLogin { get; init; }
    /// <summary>
    /// The user id of the Guest Star guest whose state was updated.
    /// This is <see langword="null"/> if the guest's slot is now empty.
    /// </summary>
    public UserId? GuestUserId { get; init; }
    /// <summary>
    /// The display name of the Guest Star guest whose state was updated.
    /// This is <see langword="null"/> if the guest's slot is now empty.
    /// </summary>
    public UserName? GuestUserName { get; init; }
    /// <summary>
    /// The login (username) of the Guest Star guest whose state was updated.
    /// This is <see langword="null"/> if the guest's slot is now empty.
    /// </summary>
    public UserLogin? GuestUserLogin { get; init; }
    /// <summary>
    /// The id of the slot the guest is assigned to.
    /// This is <see langword="null"/> if the <see cref="State"/> is
    /// <see cref="GuestStarGuestState.Invited"/>, <see cref="GuestStarGuestState.Removed"/>,
    /// <see cref="GuestStarGuestState.Ready"/>, or <see cref="GuestStarGuestState.Accepted"/>.
    /// </summary>
    public GuestStarSlotId? SlotId { get; init; }
    /// <summary>
    /// The current state of the guest after the update.
    /// This is <see langword="null"/> is the slot is now empty.
    /// </summary>
    public GuestStarGuestState? State { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required UserId HostUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required UserName HostUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required UserLogin HostUserLogin { get; init; }
    /// <summary>
    /// Indicates whether the host is allowing the guest's video to be seen by session participants.
    /// This is <see langword="null"/> if the guest is not in a slot.
    /// </summary>
    public bool? HostVideoEnabled { get; init; }
    /// <summary>
    /// Indicates whether the host is allowing the guest's audio to be heard by session participants.
    /// This is <see langword="null"/> if the guest is not in a slot.
    /// </summary>
    public bool? HostAudioEnabled { get; init; }
    /// <summary>
    /// The guest's audio level as controlled by the host, ranging from <c>0-100</c>.
    /// This is <see langword="null"/> if the guest is not in a slot.
    /// </summary>
    public GuestStarVolume? HostVolume { get; init; }
}
