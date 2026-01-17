using TwitchySharp.Api.Models.Helix.Polls.Models;

namespace TwitchySharp.Api.Models.Helix.Polls.Responses;
/// <summary>
/// Contains information about the ended poll.
/// </summary>
public record EndPollResponse
{
    /// <summary>
    /// A list containing the poll that was ended.
    /// </summary>
    public required ChatPoll[] Data { get; init; }
}
