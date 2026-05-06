using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Contains static definitions for possible chat poll statuses.
/// </summary>
/// <param name="Value">The string value of the chat poll status.</param>
[Wrapper<string>]
public readonly partial record struct ChatPollStatus(string Value)
{
    /// <summary>
    /// The poll is running.
    /// </summary>
    public static ChatPollStatus Active { get; } = new("ACTIVE");
    /// <summary>
    /// The poll ended on schedule.
    /// </summary>
    public static ChatPollStatus Completed { get; } = new("COMPLETED");
    /// <summary>
    /// The poll was terminated before its scheduled end.
    /// </summary>
    public static ChatPollStatus Terminated { get; } = new("TERMINATED");
    /// <summary>
    /// The poll has been archived and is no longer visible on the channel.
    /// </summary>
    public static ChatPollStatus Archived { get; } = new("ARCHIVED");
    /// <summary>
    /// The poll was deleted.
    /// </summary>
    public static ChatPollStatus Moderated { get; } = new("MODERATED");
    /// <summary>
    /// Something went wrong while determining the state.
    /// </summary>
    public static ChatPollStatus Invalid { get; } = new("INVALID");
}
