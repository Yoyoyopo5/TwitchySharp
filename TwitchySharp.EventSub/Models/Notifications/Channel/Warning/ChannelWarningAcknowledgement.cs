using TwitchySharp.EventSub.Interfaces.Events;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Models.Notifications.Channel.Warning;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelWarningAcknowledgement"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelwarningacknowledge">Channel Warning Acknowledgement</see> for more information.
/// </remarks>
public record ChannelWarningAcknowledgementNotification : EventSubNotification<ChannelWarningAcknowledgementEvent, ChannelWarningAcknowledgementCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelWarningAcknowledgement"/>.
/// </summary>
public record ChannelWarningAcknowledgementCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelWarningAcknowledgement"/> event.
/// </summary>
public record ChannelWarningAcknowledgementEvent : IHaveBroadcaster, IHaveUser
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
    /// The id of the user that acknowledged the warning.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that acknowledged the warning.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that acknowledged the warning.
    /// </summary>
    public required string UserName { get; init; }
}
