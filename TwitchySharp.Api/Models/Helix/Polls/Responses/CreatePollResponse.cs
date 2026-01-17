using TwitchySharp.Api.Models.Helix.Polls.Models;

namespace TwitchySharp.Api.Models.Helix.Polls.Responses;
/// <summary>
/// Contains a list of created polls.
/// </summary>
public record CreatePollResponse
{
    /// <summary>
    /// A list containing a single object for the poll that was created.
    /// </summary>
    public required ChatPoll[] Data { get; init; }
}
