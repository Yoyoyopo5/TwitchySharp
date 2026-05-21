namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUnbanRequestResolve"/> event.
/// </summary>
public record ChannelUnbanRequestResolveEvent
{
    /// <summary>
    /// The id of the unban request that was resolved.
    /// </summary>
    public required UnbanRequestId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) that the unban request is for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) that the unban request is for.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) that the unban request is for.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The user id of the moderator (or broadcaster) that resolved the unban request.
    /// </summary>
    public required UserId ModeratorUserId { get; init; } // Think typo in docs for name here
    /// <summary>
    /// The login (username) of the moderator (or broadcaster) that resolved the unban request.
    /// </summary>
    public required UserLogin ModeratorUserLogin { get; init; }
    /// <summary>
    /// The display name of the moderator (or broadcaster) that resolved the unban request.
    /// </summary>
    public required UserName ModeratorUserName { get; init; }
    /// <summary>
    /// The id of the user that created the unban request.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The login (username) of the user that created the unban request.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The display name of the user that created the unban request.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The message supplied by the moderator (or broadcaster) when resolving the unban request.
    /// </summary>
    public string? ResolutionText { get; init; }
    /// <summary>
    /// The status of the unban request after resolution.
    /// </summary>
    public required ChannelUnbanRequestResolutionStatus Status { get; init; }
}
