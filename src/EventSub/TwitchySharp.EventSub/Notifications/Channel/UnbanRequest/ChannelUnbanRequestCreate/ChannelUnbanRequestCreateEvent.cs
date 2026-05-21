namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelUnbanRequestCreate"/> event.
/// </summary>
public record ChannelUnbanRequestCreateEvent
{
    /// <summary>
    /// The id of the unban request that was created.
    /// </summary>
    public required UnbanRequestId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) which the unban request was created for.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) which the unban request was created for.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) which the unban request was created for.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
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
    /// The message submitted with the unban request.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The date and time the unban request was created.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
