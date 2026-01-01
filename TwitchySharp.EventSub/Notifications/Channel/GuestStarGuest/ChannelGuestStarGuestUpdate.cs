using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelGuestStarGuestUpdate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelguest_star_guestupdate">Channel Guest Star Guest Update</see> for more information.
/// </remarks>
public record ChannelGuestStarGuestUpdateNotification : EventSubNotification<ChannelGuestStarGuestUpdateEvent, ChannelGuestStarGuestUpdateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelGuestStarGuestUpdate"/>.
/// </summary>
public record ChannelGuestStarGuestUpdateCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Guest Star Guest Update notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The user id of the broadcaster or a moderator in the broadcaster's chat to get notifications on behalf of.
    /// </summary>
    public required string ModeratorUserId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelGuestStarGuestUpdate"/> event.
/// </summary>
public record ChannelGuestStarGuestUpdateEvent
{
    /// <summary>
    /// The user id of the broadcaster (channel) present in the Guest Star session who this subscription is associated with.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) present in the Guest Star session who this subscription is associated with.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) present in the Guest Star session who this subscription is associated with.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The id of the Guest Star session.
    /// </summary>
    public required string SessionId { get; init; }
    /// <summary>
    /// The user id of the moderator who updated the guest's state.
    /// This is <see langword="null"/> if the guest updated their own state.
    /// </summary>
    public string? ModeratorUserId { get; init; }
    /// <summary>
    /// The display name of the moderator who updated the guest's state.
    /// This is <see langword="null"/> if the guest updated their own state.
    /// </summary>
    public string? ModeratorUserName { get; init; }
    /// <summary>
    /// The login (username) of the moderator who updated the guest's state.
    /// This is <see langword="null"/> if the guest updated their own state.
    /// </summary>
    public string? ModeratorUserLogin { get; init; }
    /// <summary>
    /// The user id of the Guest Star guest whose state was updated.
    /// This is <see langword="null"/> if the guest's slot is now empty.
    /// </summary>
    public string? GuestUserId { get; init; }
    /// <summary>
    /// The display name of the Guest Star guest whose state was updated.
    /// This is <see langword="null"/> if the guest's slot is now empty.
    /// </summary>
    public string? GuestUserName { get; init; }
    /// <summary>
    /// The login (username) of the Guest Star guest whose state was updated.
    /// This is <see langword="null"/> if the guest's slot is now empty.
    /// </summary>
    public string? GuestUserLogin { get; init; }
    /// <summary>
    /// The id of the slot the guest is assigned to.
    /// This is <see langword="null"/> if the <see cref="State"/> is
    /// <see cref="GuestStarGuestState.Invited"/>, <see cref="GuestStarGuestState.Removed"/>,
    /// <see cref="GuestStarGuestState.Ready"/>, or <see cref="GuestStarGuestState.Accepted"/>.
    /// </summary>
    public string? SlotId { get; init; }
    /// <summary>
    /// The current state of the guest after the update.
    /// This is <see langword="null"/> is the slot is now empty.
    /// </summary>
    public GuestStarGuestState? State { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required string HostUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required string HostUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that started the Guest Star session.
    /// </summary>
    public required string HostUserLogin { get; init; }
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
    public int? HostVolume { get; init; }
}


/// <summary>
/// Contains static definitions for possible Guest Star guest states.
/// </summary>
/// <param name="Value">The string value of the state.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<GuestStarGuestState, string>))]
public record GuestStarGuestState(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The guest has transitioned to the invite queue. 
    /// This can take place when the guest was previously assigned a slot, but have been removed from the call and are sent back to the invite queue.
    /// </summary>
    public static GuestStarGuestState Invited { get; } = new("invited");
    /// <summary>
    /// The guest has accepted the invite and is currently in the process of setting up to join the session.
    /// </summary>
    public static GuestStarGuestState Accepted { get; } = new("accepted");
    /// <summary>
    /// The guest has signaled they are ready and can be assigned a slot.
    /// </summary>
    public static GuestStarGuestState Ready { get; } = new("ready");
    /// <summary>
    /// The guest has been assigned a slot in the session, 
    /// but is not currently seen live in the broadcasting software.
    /// </summary>
    public static GuestStarGuestState Backstage { get; } = new("backstage");
    /// <summary>
    /// The guest is now live in the host's broadcasting software.
    /// </summary>
    public static GuestStarGuestState Live { get; } = new("live");
    /// <summary>
    /// The guest was removed from the call or queue.
    /// </summary>
    public static GuestStarGuestState Removed { get; } = new("removed");
}
