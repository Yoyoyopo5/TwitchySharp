namespace TwitchySharp.Api.Helix.Polls;

/// <summary>
/// Possible statuses of a chat poll.
/// </summary>
public enum ChatPollStatus
{
    /// <summary>
    /// The poll is running.
    /// </summary>
    Active,
    /// <summary>
    /// The poll ended on schedule.
    /// </summary>
    Completed,
    /// <summary>
    /// The poll was terminated before its scheduled end.
    /// </summary>
    Terminated,
    /// <summary>
    /// The poll has been archived and is no longer visible on the channel.
    /// </summary>
    Archived,
    /// <summary>
    /// The poll was deleted.
    /// </summary>
    Moderated,
    /// <summary>
    /// Something went wrong while determining the state.
    /// </summary>
    Invalid
}
