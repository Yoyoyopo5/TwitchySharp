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
/// <inheritdoc cref="EventSubSubscriptionType.ChannelUnbanRequestResolve"/>
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types/#channelunban_requestresolve">Channel Unban Request Resolve</see> for more information.
/// </remarks>
public record ChannelUnbanRequestResolveNotification : EventSubNotification<ChannelUnbanRequestResolveEvent, ChannelUnbanRequestResolveCondition>;
/// <summary>
/// Contains subscription information specific to <see cref="EventSubSubscriptionType.ChannelUnbanRequestResolve"/>.
/// </summary>
public record ChannelUnbanRequestResolveCondition
{
    /// <summary>
    /// The user id of the broadcaster (channel) to get Unban Request Resolve notifications for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The user id of the broadcaster or a moderator in the broadcaster's chat.
    /// </summary>
    public required string ModeratorUserId { get; init; }
}
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUnbanRequestResolve"/> event.
/// </summary>
public record ChannelUnbanRequestResolveEvent
{
    /// <summary>
    /// The id of the unban request that was resolved.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the unban request is for.
    /// </summary>
    public required string BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the unban request is for.
    /// </summary>
    public required string BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the unban request is for.
    /// </summary>
    public required string BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator (or broadcaster) that resolved the unban request.
    /// </summary>
    public required string ModeratorUserId { get; init; } // Think typo in docs for name here
    /// <summary>
    /// The login (username) of the moderator (or broadcaster) that resolved the unban request.
    /// </summary>
    public required string ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator (or broadcaster) that resolved the unban request.
    /// </summary>
    public required string ModeratorUserName { get; init; }
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
    /// The message supplied by the moderator (or broadcaster) when resolving the unban request.
    /// </summary>
    public string? ResolutionText { get; init; }
    /// <summary>
    /// The status of the unban request after resolution.
    /// </summary>
    public required ChannelUnbanRequestResolutionStatus Status { get; init; }
}

/// <summary>
/// Contains static definitions for possible statuses for channel unban requests.
/// </summary>
/// <param name="Value">The string value of the status.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<ChannelUnbanRequestResolutionStatus, string>))]
public record ChannelUnbanRequestResolutionStatus(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// The unban request was approved by a moderator.
    /// </summary>
    public static ChannelUnbanRequestResolutionStatus Approved { get; } = new("approved");
    /// <summary>
    /// The unban request was canceled.
    /// </summary>
    /// <remarks>
    /// Dev Note: Not exactly sure when this applies. It might be that the user canceled their own request,
    /// or potentially that the user was unbanned before the request was resolved.
    /// </remarks>
    public static ChannelUnbanRequestResolutionStatus Canceled { get; } = new("canceled");
    /// <summary>
    /// The unban request was denied by a moderator.
    /// </summary>
    public static ChannelUnbanRequestResolutionStatus Denied { get; } = new("denied");
}
