using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.EventSub.Models.Conditions;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications.Channel.UnbanRequest;
/// <summary>
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUnbanRequestCreate"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelunban_requestcreate">Channel Unban Request Create</see> for more information.
/// </remarks>
public record ChannelUnbanRequestCreateNotification : EventSubNotification<ChannelUnbanRequestCreateEvent, ChannelUnbanRequestCreateCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelUnbanRequestCreate"/>.
/// </summary>
public record ChannelUnbanRequestCreateCondition : BroadcasterModeratorCondition;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUnbanRequestCreate"/> event.
/// </summary>
public record ChannelUnbanRequestCreateEvent
{
    /// <summary>
    /// The id of the unban request that was created.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) which the unban request was created for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) which the unban request was created for.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) which the unban request was created for.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The id of the user that created the unban request.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that created the unban request.
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that created the unban request.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The message submitted with the unban request.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The date and time the unban request was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
