using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Polls;

/// <summary>
/// Contains static references for valid poll end statuses.
/// </summary>
/// <param name="Value">The string value of the status to end the poll with.</param>
[Wrapper<string>]
public readonly partial record struct EndPollStatus(string Value)
{
    /// <summary>
    /// Ends the poll before the poll is scheduled to end. 
    /// The poll remains publicly visible.
    /// </summary>
    public static EndPollStatus Terminated { get; } = new("TERMINATED");
    /// <summary>
    /// Ends the poll before the poll is scheduled to end, and then archives it so it's no longer publicly visible.
    /// </summary>
    public static EndPollStatus Archived { get; } = new("ARCHIVED");
}
