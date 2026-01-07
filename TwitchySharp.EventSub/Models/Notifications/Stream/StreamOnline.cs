using TwitchySharp.EventSub.Enums.Events.Stream;
using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Stream;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.StreamOnline"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#streamonline">Stream Online</see> for more information.
/// </remarks>
public record StreamOnlineNotification : EventSubNotification<StreamOnlineEvent, StreamOnlineCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.StreamOnline"/>.
/// </summary>
public record StreamOnlineCondition : BroadcasterCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.StreamOnline"/> event.
/// </summary>
public record StreamOnlineEvent : IHaveBroadcaster
{
    /// <summary>
    /// The user id of the broadcaster (channel) whose stream went online.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) whose stream went online.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) whose stream went online.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the stream.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The stream type.
    /// </summary>
    public required StreamType Type { get; init; }
    /// <summary>
    /// The date and time when the stream went online.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
}
