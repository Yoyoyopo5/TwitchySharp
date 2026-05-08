using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Interfaces.Events.Channel.SharedChat;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.EventSub.Models.Events.Channel.SharedChat;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.SharedChat;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelSharedChatSessionBegin"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelshared_chatbegin">Channel Shared Chat Begin</see> for more information.
/// </remarks>
public record ChannelSharedChatBeginNotification : EventSubNotification<ChannelSharedChatBeginEvent, ChannelSharedChatBeginCondition>;

/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelSharedChatSessionBegin"/>.
/// </summary>
public record ChannelSharedChatBeginCondition : BroadcasterCondition;

/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelSharedChatSessionBegin"/> event.
/// </summary>
public record ChannelSharedChatBeginEvent : IHaveSharedChat, IHaveBroadcaster
{
    public required string SessionId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) from the subscription condition that is active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) from the subscription condition that is active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) from the subscription condition that is active in the shared chat session.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    public required string HostBroadcasterUserId { get; init; }
    public required string HostBroadcasterUserName { get; init; }
    public required string HostBroadcasterUserLogin { get; init; }
    public required SharedChatParticipant[] Participant { get; init; }
}
