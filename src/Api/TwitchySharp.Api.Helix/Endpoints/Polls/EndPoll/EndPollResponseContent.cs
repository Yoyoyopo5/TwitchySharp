namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Contains information about the ended poll.
/// </summary>
public record EndPollResponseContent
{
    /// <summary>
    /// A list containing the poll that was ended.
    /// </summary>
    public required ChatPoll[] Data { get; init; }
}
