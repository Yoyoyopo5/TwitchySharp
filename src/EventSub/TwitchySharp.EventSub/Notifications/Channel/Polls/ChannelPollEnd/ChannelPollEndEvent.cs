namespace TwitchySharp.EventSub.Notifications;
/// <summary>
/// Contains information about a specific <see cref="EventSubSubscriptionType.ChannelPollEnd"/> event.
/// </summary>
public record ChannelPollEndEvent
{
    /// <summary>
    /// The id of the poll.
    /// </summary>
    public required PollId Id { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required UserId BroadcasterUserId { get; init; }
    /// <summary>
    /// The login (username) of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required UserLogin BroadcasterUserLogin { get; init; }
    /// <summary>
    /// The display name of the broadcaster (channel) hosting the poll.
    /// </summary>
    public required UserName BroadcasterUserName { get; init; }
    /// <summary>
    /// The title of the poll.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// The choices for the poll, including their vote count.
    /// </summary>
    public required ChannelPollChoice[] Choices { get; init; }
    /// <summary>
    /// The setting for Channel Points voting.
    /// </summary>
    public required ChannelPollChannelPointsVotingSetting ChannelPointsVoting { get; init; }
    /// <summary>
    /// The date and time the poll began.
    /// </summary>
    public required DateTimeOffset StartedAt { get; init; }
    /// <summary>
    /// The date and time when the poll ended.
    /// </summary>
    public required DateTimeOffset EndedAt { get; init; }
}
